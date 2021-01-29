using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Application.Status;
using MediatR;

namespace Application.Courses
{
    public class UpdateCategory
    {
        public class Command : IRequest<StatusAction>
        {
            public Guid CategoryId { get; set; }

            public Guid CourseId { get; set; }
        }

        public class Handler : IRequestHandler<Command, StatusAction>
        {
            private readonly ICourseServices services;

            public Handler(ICourseServices courseServices)
            {
                this.services = courseServices;
            }

            public async Task<StatusAction> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var item = await this.services.GetByIdEntityAsync(request.CourseId);
                    if (item == null)
                        throw new Exception("Khóa học không tồn tại");
                    var result = await this.services.UpdateCategory(request.CourseId, request.CategoryId);
                    if (result)
                        return new UpdateSuccess("chủ đề khóa học");
                    else
                        throw new Exception("Cập nhật thất bại");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }
    }
}