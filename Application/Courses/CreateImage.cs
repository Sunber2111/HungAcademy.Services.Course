using Application.Constants;
using Application.Errors;
using Application.Services.Interfaces;
using Application.Status;
using FluentValidation;
using Infrastructure.Photos;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses
{
    public class CreateImage
    {
        public class Command : IRequest<StatusAction>
        {
            public IFormFile File { get; set; }

            public Guid CourseId { get; set; }
        }

         public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.File).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu hình ảnh. Xin hãy tải hình lên" }));
                
                RuleFor(x => x.CourseId).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu tên khóa học. Xin hãy nhập mã" }));
                
            }
        }
        
        public class Handler : IRequestHandler<Command, StatusAction>
        {
            private readonly IPhotoAccessor photoAccessor;

            private readonly ICourseServices coursesServices;

            public Handler(IPhotoAccessor photoAccessor, ICourseServices coursesServices)
            {
                this.photoAccessor = photoAccessor;
                this.coursesServices = coursesServices;
            }
        
            public async Task<StatusAction> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var course = await this.coursesServices.GetByIdAsync(request.CourseId);

                    if (course == null)
                        throw new NotFoundException("Không tìm thấy khóa học");

                    var photoUploadResult = new PhotoUploadResult();

                    if (course.PhotoId == null)
                    {
                        photoUploadResult = photoAccessor.AddPhoto(request.File);

                        course.Photo = photoUploadResult.Url;

                        course.PhotoId = photoUploadResult.PublicId;

                        await this.coursesServices.UpdateAsync(course, course.Id);
                    }
                    else
                    {
                        photoAccessor.DeletePhoto(course.PhotoId);

                        photoUploadResult = photoAccessor.AddPhoto(request.File);

                        course.Photo = photoUploadResult.Url;

                        course.PhotoId = photoUploadResult.PublicId;

                        await this.coursesServices.UpdateAsync(course, course.Id);
                    }

                    return new UpdateImageSuccess() { 
                        Photo = photoUploadResult.Url
                    };


                }
                catch (Exception ex)
                {
                    if(ex.GetType() == typeof(NotFoundException))
                    {
                        throw new RestException(StatusCode.BadRequest,ex.Message);
                    }
                    throw new RestException(StatusCode.BadRequest, "Cập nhật thất bại");
                }
        
            }
        }
    }
}