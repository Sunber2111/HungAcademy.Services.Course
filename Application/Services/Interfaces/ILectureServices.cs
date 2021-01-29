using System;
using System.Threading.Tasks;
using Application.Lectures;
using Domain;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Interfaces
{
    public interface ILectureServices : IBaseServices<LectureDto, Lecture>
    {
        Task<string> UpdateVideo(Guid lectureId, string videoTime,IFormFile video);
    }
}