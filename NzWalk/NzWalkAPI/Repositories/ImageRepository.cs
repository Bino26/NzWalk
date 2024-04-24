using NzWalkAPI.Data;
using NzWalkAPI.Models.Domain;

namespace NzWalkAPI.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly NzWalkDbContext nzWalkDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ImageRepository( NzWalkDbContext nzWalkDbContext , IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor)
        {
            this.nzWalkDbContext = nzWalkDbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            await nzWalkDbContext.Images.AddAsync(image);
            await nzWalkDbContext.SaveChangesAsync();
            return image;

            
        }
    }
}
