using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Services.Interfaces;
using Application.Status;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.UserWatchers
{
    public class Create
    {
        public class Command : IRequest<StatusAction>
        {
            public Guid LectureId { get; set; }

            public Guid UserId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.LectureId).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mã bài học. Xin hãy nhập mã bài học" }));

                RuleFor(x => x.UserId).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mã khách hàng. Xin hãy nhập mã khách hàng" }));
            }
        }


        public class Handler : IRequestHandler<Command, StatusAction>
        {
            private readonly IUserWatcherServices services;

            public Handler(IUserWatcherServices services)
            {
                this.services = services;
            }

            public async Task<StatusAction> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await this.services.InsertEntityAsync(new UserWatcher()
                    {
                        UserId = request.UserId,
                        LectureId = request.LectureId
                    });
                    return new CreateSuccess();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}