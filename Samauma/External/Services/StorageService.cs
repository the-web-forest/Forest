using System;
using System.Text.RegularExpressions;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Samauma.UseCases.Interfaces.Services;

namespace Samauma.External.Services
{
    public class StorageService: IStorageService
    {
        private readonly IConfiguration _configuration;
        
        public StorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public string UploadTreeImageInBase64(string Base64Image)
        {
            return UploadImageInBase64(Base64Image, _configuration["Storage:TreeContainerName"]);
        }

        private string UploadImageInBase64(string Base64Image, string ContainerName)
        {
            var DefaultExtension = "png";
            var FileName = Guid.NewGuid().ToString() + "." + DefaultExtension;
            var BlobClient = new BlobClient(_configuration["Storage:ConnectionString"], ContainerName, FileName);
            var Data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(Base64Image, "");
            byte[] ImageBytes = Convert.FromBase64String(Data);

            var Options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders()
                {
                    ContentType = "image/" + DefaultExtension
                }
            };

            using (var Stream = new MemoryStream(ImageBytes))
            {
                BlobClient.Upload(Stream, Options);
            }

            return BlobClient.Uri.AbsoluteUri;
        }
    }
}

