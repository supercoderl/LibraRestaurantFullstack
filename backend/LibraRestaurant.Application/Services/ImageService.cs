using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using LibraRestaurant.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.Services
{
    public class ImageService : IImageService
    {
        private Cloudinary _cloudinary;

        public ImageService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        //Send file to cloud and get url string
        public async Task<string> UploadFile(string base64, string fileName, string folder)
        {
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(fileName, base64),
                    Folder = folder
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.Url.ToString();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
