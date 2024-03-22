using NzWalkAPI.Models.Domain;

namespace NzWalkAPI.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateWalkAsync(Walk walk);
        Task<List<Walk>> GetWalkAsync();
        Task<Walk> GetWalkByIdAsync(Guid id);
        Task<Walk> UpdateWalkAsync(Guid id ,Walk walk);
        Task<Walk> DeleteWalkAsync(Guid id);
    }
}
