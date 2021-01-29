using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Errors;
using Application.Services.Interfaces;
using Application.Status;
using FluentValidation;
using MediatR;

namespace Application.Categories
{
    public class Update
    {
        public class Command : IRequest<StatusAction>
        {
            public Guid Id { get; set; }

            public string Name { get; set; }
        }

         public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mã chủ đề khóa học. Xin hãy nhập mã" }));
                
                RuleFor(x => x.Name).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu tên chủ đề khóa học. Xin hãy tên" }));
                
            }
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
                    var category = new CategoryDto
                    {
                        Id = request.Id,
                        Name = request.Name
                    };

                    await this.services.UpdateAsync(category, request.Id);

                    return new UpdateSuccess("chủ đề khóa học");

                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(NotFoundException))
                    {
                        throw new RestException(StatusCode.BadRequest, new { message="Chủ đề khóa học không tồn tại" });
                    }
                    throw new RestException(StatusCode.BadRequest,new { message = "Cập nhật thất bại" });
                }
            }
        }
    }
}