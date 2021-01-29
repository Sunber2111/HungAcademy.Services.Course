using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Errors;
using Application.Services.Interfaces;
using MediatR;

namespace Application.Sections
{
    public class List
    {
        public class Query : IRequest<IEnumerable<SectionDto>>
        {

        }

        public class Handler : IRequestHandler<Query, IEnumerable<SectionDto>>
        {
            private readonly ISectionServices services;
            public Handler(ISectionServices services)
            {
                this.services = services;
            }

            public async Task<IEnumerable<SectionDto>> Handle(Query request, CancellationToken cancellationToken)
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