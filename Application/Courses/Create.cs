using Application.Errors;
using Application.Services.Interfaces;
using Application.Status;
using Domain;
using FluentValidation;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses
{
    public class Create
    {
        public class Command : IRequest<StatusAction>
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Title { get; set; }

            public string Photo { get; set; }

            public double Price { get; set; }

            public DateTime? CreateDate { get; set; }
            
            public float Percent { get; set; }

            public bool IsSale { get; set; }

            public string ConditionRequirement { get; set; }

            public string MainContent { get; set; }

            public string CustomerSegment { get; set; }

            public Guid? CategoryId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu tiêu đề. Xin hãy nhập tiêu đề" }));
                
                RuleFor(x => x.Name).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu tên khóa học. Xin hãy nhập tiêu đề" }));
                
            }
        }

        public class Handler : IRequestHandler<Command, StatusAction>
        {
            private readonly ICourseServices courseServices;

            public Handler(ICourseServices courseServices)
            {
                this.courseServices = courseServices;
            }

            public async Task<StatusAction> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var course = new Course()
                    {
                        Name = request.Name,
                        Title = request.Title,
                        Photo = request.Photo,
                        Price = request.Price,
                        IsSale = request.IsSale,
                        ConditionRequirement = request.ConditionRequirement,
                        MainContent = request.MainContent,
                        CustomerSegment = request.CustomerSegment,
                        CategoryId = request.CategoryId
                    };

                    course.CreateDate = request.CreateDate ?? DateTime.Now;

                    var item = await this.courseServices.InsertAsync(course);

                    return new CreateSuccessWithId(item.Id, "khóa học");

                }
                catch (Exception)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { message = "Thêm Khóa học không thành công" });
                }
            }
        }

    }
}
