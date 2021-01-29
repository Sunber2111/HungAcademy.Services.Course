using System;
using System.Threading.Tasks;
using Application.Errors;
using Application.Lectures;
using Application.Services.Interfaces;
using Domain;
using Infrastructure.Videos;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Implements
{
    public class LectureServices : BaseServices<LectureDto, Lecture>, ILectureServices
    {
        private readonly IVideoAccessor videoAccessor;

        public LectureServices(IServiceProvider services, IVideoAccessor videoAccessor) : base(services)
        {
            this.videoAccessor = videoAccessor;
        }

        public async Task<string> UpdateVideo(Guid lectureId, string videoTime, IFormFile video)
        {
            try
            {
                using (var unitOfWork = NewDbContext())
                {
                    var lecture = await GetByIdEntityAsync(lectureId);

                    if (lecture == null)
                        throw new NotFoundException();

                    if (lecture.VideoId != null)
                        this.videoAccessor.DeleteVideo(lecture.VideoId);

                    var updateVideoResult = this.videoAccessor.AddVideo(video);

                    lecture.Video = updateVideoResult.Url;

                    lecture.VideoId = updateVideoResult.PublicId;

                    lecture.VideoTime = videoTime;

                    var result = await unitOfWork.SaveChangeAsync();

                    if (result)
                        return updateVideoResult.Url;

                    throw new Exception("Cập nhật thất bại");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}