using Microsoft.AspNetCore.Http;

namespace Infrastructure.Photos
{
    public interface IPhotoAccessor
    {
        PhotoUploadResult AddPhoto(IFormFile file);
        string DeletePhoto(string publicId);

    }
}
