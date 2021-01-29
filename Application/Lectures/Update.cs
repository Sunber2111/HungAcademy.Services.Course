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

namespace Application.Lectures
{
    public class Update
    {
        public class Command : IRequest<StatusAction>
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu tên bài học. Xin hãy nhập tên khóa học" }));

                RuleFor(x => x.Description).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mô tả học. Xin hãy nhập mô tả" }));

                RuleFor(x => x.Id).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mã bài học. Xin hãy nhập mã bài học" }));
            }
        }

        public class Handler : IRequestHandler<Command, StatusAction>
        {
            private readonly ILectureServices services;

            public Handler(ILectureServices services)
            {
                this.services = services;
            }

            public async Task<StatusAction> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var lec = new LectureDto
                    {
                        Id = request.Id,
                        Name = request.Name,
                        Description = request.Description
                    };

                    await this.services.UpdateAsync(lec, request.Id);
                    return new UpdateSuccess("bài học");
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(NotFoundException))
                    {
                        throw new RestException(StatusCode.BadRequest, new { message = "Bài học không tồn tại" });
                    }
                    throw new RestException(StatusCode.BadRequest, new { message = "Cập nhật thất bại" });
                }
            }
        }
    }
}