using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Infrastructure.Photos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Videos
{
    public class VideoAccessor : IVideoAccessor
    {
         private readonly Cloudinary _cloudinary;

        public VideoAccessor(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [Obsolete]
        public PhotoUploadResult AddVideo(IFormFile file)
        {
             var uploadResult = new VideoUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {

                    var uploadParams = new VideoUploadParams
                    {
                        File = new FileDescription(file.FileName, stream)
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            if (uploadResult.Error != null)
                throw new Exception(uploadResult.Error.Message);

            return new PhotoUploadResult
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUri.AbsoluteUri
            };
        }

        public string DeleteVideo(string publicId)
        {
            throw new System.NotImplementedException();
        }
    }
}