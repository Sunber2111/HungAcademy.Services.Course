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

namespace Application.CaseStudies
{
    public class Update
    {
        public class Command : IRequest<StatusAction>
        {
            public Guid Id { get; set; }

            public string Description { get; set; }
        }

         public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mã . Xin hãy nhập mã" }));
                
                RuleFor(x => x.Description).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mô tả khóa học. Xin hãy nhập mô tả" }));
                
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
                    var sec = new CaseStudyDto
                    {
                        Id = request.Id,
                        Description = request.Description
                    };

                    await this.services.UpdateAsync(sec, request.Id);

                    return new UpdateSuccess("khóa học");
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(NotFoundException))
                    {
                        throw new RestException(StatusCode.BadRequest, new { message = "Mục tiêu khóa học không tồn tại" });
                    }
                    throw new RestException(StatusCode.BadRequest, new { message = "Cập nhật thất bại" });
                }

            }
        }
    }
}