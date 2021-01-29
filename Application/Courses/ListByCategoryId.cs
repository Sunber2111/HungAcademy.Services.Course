using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Errors;
using Application.Services.Interfaces;
using MediatR;

namespace Application.Courses
{
    public class ListByCategoryId
    {
        public class Query : IRequest<List<CourseDto>>
        {
            public Guid CategoryId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<CourseDto>>
        {
            private readonly ICourseServices services;

            public Handler(ICourseServices services)
            {
                this.services = services;
            }

            public async Task<List<CourseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    return await this.services.GetAsync(x => x.CategoryId == request.CategoryId);

                }
                catch (Exception)
                {
                    throw new RestException(StatusCode.BadRequest, new { message = "Thất bại" });
                }

            }
        }
    }
}