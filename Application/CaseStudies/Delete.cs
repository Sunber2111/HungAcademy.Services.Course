using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Errors;
using Application.Services.Interfaces;
using Application.Status;
using MediatR;

namespace Application.CaseStudies
{
    public class Delete
    {
        public class Command : IRequest<StatusAction>
        {
            public Guid Id { get; set; }
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
                    await this.services.DeleteAsync(request.Id);
                    return new DeleteSuccess("mục tiêu khóa học");
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(NotFoundException))
                    {
                        throw new RestException(StatusCode.BadRequest, new { message = "Học phần không tồn tại" });
                    }
                    throw new RestException(StatusCode.BadRequest, new { message = "Xóa học phần thất bại" });
                }
            }
        }
    }
}