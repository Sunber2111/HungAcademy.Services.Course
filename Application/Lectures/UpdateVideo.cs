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
using Microsoft.AspNetCore.Http;

namespace Application.Lectures
{
    public class UpdateVideo
    {
        public class Command : IRequest<StatusAction>
        {
            public Guid LectureId { get; set; }         

            public IFormFile Video { get; set; }

            public string VideoTime { get; set; }
        }
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.LectureId).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu mã bài học. Xin hãy nhập mã bài học" }));

                RuleFor(x => x.Video).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu video. Xin hãy tải video" }));

                RuleFor(x => x.VideoTime).NotEmpty()
                .OnFailure((obj, context, errorMessage) => throw new RestException(HttpStatusCode.BadRequest, new { message = "Thiếu thời lượng video. Xin hãy nhập thời lượng video" }));
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
                    var videoUrl = await this.services.UpdateVideo(request.LectureId, request.VideoTime, request.Video);

                    return new UpdateVideoSuccess(videoUrl);

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