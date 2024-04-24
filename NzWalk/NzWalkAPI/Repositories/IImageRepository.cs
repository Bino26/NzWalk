using NzWalkAPI.Models.Domain;

namespace NzWalkAPI.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
