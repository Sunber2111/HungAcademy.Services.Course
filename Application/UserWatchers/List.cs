using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Errors;
using Application.Services.Interfaces;
using MediatR;

namespace Application.UserWatchers
{
    public class List
    {
        public class Command : IRequest<UserWatcherDto>
        {
            public Guid UserId { get; set; }

            public Guid CourseId { get; set; }
        }

        public class Handler : IRequestHandler<Command, UserWatcherDto>
        {
            private readonly IUserWatcherServices services;

            public Handler(IUserWatcherServices services)
            {
                this.services = services;
            }

            public async Task<UserWatcherDto> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    return await this.services.GetUserWatcher(request.CourseId, request.UserId);
                }
                catch (Exception)
                {
                    throw new RestException(StatusCode.BadRequest, new { message = "Không tìm thấy" });
                }

            }
        }
    }
}