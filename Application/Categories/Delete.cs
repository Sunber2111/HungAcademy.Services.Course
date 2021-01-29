using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Errors;
using Application.Services.Interfaces;
using Application.Status;
using MediatR;

namespace Application.Categories
{
    public class Delete
    {
        public class Command : IRequest<StatusAction>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, StatusAction>
        {
            private readonly ICategoryServices services;

            public Handler(ICategoryServices services)
            {
                this.services = services;
            }

            public async Task<StatusAction> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    await this.services.DeleteAsync(request.Id);
                    return new DeleteSuccess("chủ đề khóa học");
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(NotFoundException))
                    {
                        throw new RestException(StatusCode.BadRequest, new { message = "Chủ đề khóa học không tồn tại" });
                    }
                    
                    throw new RestException(StatusCode.BadRequest, new { message = "Xóa chủ đề khóa học thất bại" });
                }

            }
        }
    }
}