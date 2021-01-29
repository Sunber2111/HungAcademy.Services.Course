using Application.Constants;
using Application.Errors;
using Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses
{
    public class List
    {
        public class Query : IRequest<List<CourseDto>>
        {

        }

        public class Handler : IRequestHandler<Query, List<CourseDto>>
        {
            private readonly ICourseServices services;

            public Handler(ICourseServices courseServices)
            {
                this.services = courseServices;
            }

            public async Task<List<CourseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    return await this.services.GetAllAsync();
                }
                catch (Exception)
                {
                    throw new RestException(StatusCode.BadRequest, new { message = "Thất bại" });
                }
            }
        }
    }
}
