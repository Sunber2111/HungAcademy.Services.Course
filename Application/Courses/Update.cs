using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Errors;
using Application.Services.Interfaces;
using Application.Status;
using FluentValidation;
using MediatR;

namespace Application.Courses
{
    public class Update
    {
        public class Command : IRequest<StatusAction>
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Title { get; set; }

            public string Photo { get; set; }

            public double Price { get; set; }

            public DateTime? CreateDate { get; set; }

            public bool IsSale { get; set; }

            public string ConditionRequirement { get; set; }

            public string MainContent { get; set; }

            public string CustomerSegment { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mã khóa học. Xin hãy nhập mã khóa học" }));

                RuleFor(x => x.Title).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu tiêu đề khóa học. Xin hãy nhập tiêu đề khóa học" }));

                 RuleFor(x => x.Name).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu tên khóa học. Xin hãy nhập tên khóa học" }));
            }
        }

        public class Handler : IRequestHandler<Command, StatusAction>
        {
            private readonly ICourseServices services;

            public Handler(ICourseServices services)
            {
                this.services = services;
            }

            public async Task<StatusAction> Handle(Command request, CancellationToken cancellationToken)
            {
                // handler logic     
                try
                {
                    var course = new CourseDto
                    {
                        Id = request.Id,
                        Name = request.Name,
                        Title = request.Title,
                        Photo = request.Photo,
                        Price = request.Price,
                        IsSale = request.IsSale,
                        ConditionRequirement = request.ConditionRequirement,
                        MainContent = request.MainContent,
                        CustomerSegment = request.CustomerSegment
                    };
                    course.CreateDate = request.CreateDate ?? DateTime.Now;

                    await services.UpdateAsync(course, course.Id);

                    return new UpdateSuccess("khóa học");

                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(NotFoundException))
                    {
                        throw new RestException(StatusCode.BadRequest, new { message = "Chủ đề khóa học không tồn tại" });
                    }
                    throw new RestException(StatusCode.BadRequest, new { message = "Cập nhật thất bại" });
                }

            }
        }
    }
}
