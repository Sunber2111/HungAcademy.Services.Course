using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Errors;
using Application.Services.Interfaces;
using Application.Status;
using MediatR;

namespace Application.Courses
{
    public class Delete
    {
        public class Command : IRequest<StatusAction>
        {
            public Guid Id { get; set; }
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
                try
                {
                    await this.services.DeleteAsync(request.Id);
                    return new DeleteSuccess("khóa học");
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(NotFoundException))
                    {
                        throw new RestException(StatusCode.BadRequest, new { message = "Khóa học không tồn tại" });
                    }
                    throw new RestException(StatusCode.BadRequest, new { message = "Xóa khóa học thất bại" });
                }
            }
        }
    }
}
