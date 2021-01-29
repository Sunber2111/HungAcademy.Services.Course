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
using MediatR;

namespace Application.CaseStudies
{
    public class Create
    {
        public class Command : IRequest<StatusAction>
        {
            public string Description { get; set; }

            public Guid CourseId { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Description).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mô tả. Xin hãy nhập mô tả" }));
                
                RuleFor(x => x.CourseId).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mã khóa học. Xin hãy nhập mã" }));
                
            }
        }

        public class Handler : IRequestHandler<Command, StatusAction>
        {
            private readonly ICaseStudyServices services;

            public Handler(ICaseStudyServices services)
            {
                this.services = services;
            }

            public async Task<StatusAction> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await this.services.InsertEntityAsync(new CaseStudy
                    {
                        Description = request.Description,
                        CourseId = request.CourseId
                    });

                    return new CreateSuccessWithId(result.Id);

                }
                catch (Exception)
                {
                    throw new RestException(StatusCode.BadRequest, new { message = "Thêm mục tiêu khóa học thất bại" });
                }

            }
        }
    }
}