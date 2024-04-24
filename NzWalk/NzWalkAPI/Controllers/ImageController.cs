using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalkAPI.Models.Domain;
using NzWalkAPI.Models.DTO;
using NzWalkAPI.Repositories;

namespace NzWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Uploadimage")]

        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequest)
        {
            ValidateFileUpload(imageUploadRequest);
            if (ModelState.IsValid)
            {
                // convert DTO to Domain model
                var imageDomainModel = new Image
                {
                    File = imageUploadRequest.File,
                    FileExtension = Path.GetExtension(imageUploadRequest.File.FileName),
                    FileSizeInBytes = imageUploadRequest.File.Length,
                    FileName = imageUploadRequest.FileName,
                    FileDescription = imageUploadRequest.FileDescription,
                };

                //Upload image to the database

                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);



            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequest)
        {
            var allowedExtensions = new string[] { ".jpg",".jpeg",".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequest.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extensions");
            }
            if (imageUploadRequest.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");

            }

        }
    }
}
