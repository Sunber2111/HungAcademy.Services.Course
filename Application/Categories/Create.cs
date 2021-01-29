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
    public class Create
    {
        public class Command : IRequest<StatusAction>
        {
            public string Name { get; set; }
        }

         public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu tên chủ đề khóa học. Xin hãy nhập tên" }));

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
                    var cate = new CategoryDto
                    {
                        Name = request.Name
                    };

                    var item = await this.services.InsertAsync(cate);

                    return new CreateSuccessWithId(item.Id, "chủ đề khóa học");

                }
                catch
                {
                    throw new RestException(StatusCode.BadRequest, new { message = "Thêm chủ đề khóa học thất bại" });
                }

            }
        }
    }
}
