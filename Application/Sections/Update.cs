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
    public class Update
    {
        public class Command : IRequest<StatusAction>
        {
            public Guid Id { get; set; }

            public string Name { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mã phần học. Xin hãy nhập mã phần học" }));

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
                    var sec = new SectionDto
                    {
                        Id = request.Id,
                        Name = request.Name
                    };

                    await this.services.UpdateAsync(sec, request.Id);

                    return new UpdateSuccess("khóa học");
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(NotFoundException))
                    {
                        throw new RestException(StatusCode.BadRequest, new { message = "Phần học không tồn tại" });
                    }
                    throw new RestException(StatusCode.BadRequest, new { message = "Cập nhật thất bại" });
                }
            }
        }
    }
}