using Application.Constants;
using Application.Errors;
using Application.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Categories
{
    public class List
    {

        public class Query : IRequest<List<CategoryDto>>
        {

        }

        public class Handler : IRequestHandler<Query, List<CategoryDto>>
        {
            private readonly ICategoryServices services;

            public Handler(ICategoryServices services)
            {
                this.services = services;
            }

            public async Task<List<CategoryDto>> Handle(Query request, CancellationToken cancellationToken)
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
