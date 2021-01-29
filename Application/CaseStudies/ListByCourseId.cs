using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Errors;
using Application.Services.Interfaces;
using MediatR;

namespace Application.CaseStudies
{
    public class ListByCourseId
    {
        public class Command : IRequest<List<CaseStudyDto>>
        {
            public Guid CourseId { get; set; }
        }

        public class Handler : IRequestHandler<Command, List<CaseStudyDto>>
        {
            private readonly ICaseStudyServices services;

            public Handler(ICaseStudyServices services)
            {
                this.services = services;
            }

            public async Task<List<CaseStudyDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    return await this.services.GetByCourseId(request.CourseId);
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(NotFoundException))
                    {
                        throw new RestException(StatusCode.BadRequest, new { message = "Khóa học không tồn tại" });
                    }
                    throw new RestException(StatusCode.BadRequest, new { message = "Cập nhật thất bại" });
                }

            }
        }
    }
}
