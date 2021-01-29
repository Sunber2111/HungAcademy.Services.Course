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

namespace Application.Sections
{
    public class Create
    {
        public class Command : IRequest<StatusAction>
        {
            public Guid CourseId { get; set; }

            public string Name { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CourseId).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mã khóa học. Xin hãy nhập mã khóa học" }));

                RuleFor(x => x.Name).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu tên phần học. Xin hãy tải tên phần học" }));
            }
        }

        public class Handler : IRequestHandler<Command, StatusAction>
        {
            private readonly ISectionServices services;

            public Handler(ISectionServices services)
            {
                this.services = services;
            }

            public async Task<StatusAction> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await this.services.InsertSection(request.Name, request.CourseId);

                    return new CreateSuccessWithId(result, "phần học");
                }
                catch (Exception ex)
                {
                    throw new RestException(StatusCode.BadRequest, new { message = ex.Message });
                }

            }
        }
    }
}
