using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Errors;
using Application.Services.Interfaces;
using Application.Status;
using Domain;
using FluentValidation;
using Infrastructure.Photos;
using Infrastructure.Videos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Lectures
{
    public class Create
    {
        public class Command : IRequest<StatusAction>
        {
            public string Name { get; set; }

            public string Description { get; set; }

            public Guid SectionId { get; set; }

            public string VideoTime { get; set; }

            public IFormFile Video { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu tên bài học. Xin hãy nhập tên khóa học" }));

                RuleFor(x => x.Description).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mô tả học. Xin hãy nhập mô tả" }));

                RuleFor(x => x.SectionId).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mã phần học. Xin hãy nhập mã phần học" }));
            }
        }

        public class Handler : IRequestHandler<Command, StatusAction>
        {
            private readonly ILectureServices services;

            private readonly IVideoAccessor videoAccessor;

            public Handler(ILectureServices services, IVideoAccessor videoAccessor)
            {
                this.videoAccessor = videoAccessor;

                this.services = services;
            }

            public async Task<StatusAction> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var uploadVideoResult = new PhotoUploadResult();
                    if (request.Video != null)
                        uploadVideoResult = this.videoAccessor.AddVideo(request.Video);

                    var result = await this.services.InsertEntityAsync(new Lecture
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Video = uploadVideoResult.Url,
                        VideoId = uploadVideoResult.PublicId,
                        SectionId = request.SectionId,
                        VideoTime = request.VideoTime
                    });

                    return new CreateLectureSuccess(result.Id, result.Video, "bài học");
                }
                catch (Exception)
                {
                    throw new RestException(StatusCode.BadRequest, new { message = "Thêm bài học thất bại" });
                }

            }
        }
    }
}