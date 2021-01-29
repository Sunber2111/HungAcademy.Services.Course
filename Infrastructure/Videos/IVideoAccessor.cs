using Infrastructure.Photos;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Videos
{
    public interface IVideoAccessor
    {
        PhotoUploadResult AddVideo(IFormFile file);
        string DeleteVideo(string publicId);
    }
}