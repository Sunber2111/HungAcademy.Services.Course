using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Services.Interfaces;
using MediatR;

namespace Application.Courses
{
    public class Detail
    {
        public class Query : IRequest<CourseDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, CourseDto>
        {
            private readonly ICourseServices services;
            public Handler(ICourseServices services)
            {
                this.services = services;
            }

            public async Task<CourseDto> Handle(Query request, CancellationToken cancellationToken)
            {
                // handler logic goes here
                var item = await services.GetByIdAsync(request.Id);
                if (item == null)
                    throw new RestException(HttpStatusCode.BadRequest, new { message = "Không tìm thấy khóa học" });
                return item;
            }
        }
    }
}