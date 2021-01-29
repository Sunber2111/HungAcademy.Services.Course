using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Errors;
using Application.Services.Interfaces;
using MediatR;

namespace Application.Lectures
{
    public class List
    {
        public class Query : IRequest<IEnumerable<LectureDto>>
        {

        }

        public class Handler : IRequestHandler<Query, IEnumerable<LectureDto>>
        {
            private readonly ILectureServices services;
            public Handler(ILectureServices services)
            {
                this.services = services;
            }

            public async Task<IEnumerable<LectureDto>> Handle(Query request, CancellationToken cancellationToken)
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