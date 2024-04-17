using NzWalkAPI.Models.Domain;

namespace NzWalkAPI.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateWalkAsync(Walk walk);
        Task<List<Walk>> GetWalkAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending =true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk> GetWalkByIdAsync(Guid id);
        Task<Walk> UpdateWalkAsync(Guid id ,Walk walk);
        Task<Walk> DeleteWalkAsync(Guid id);
        Task<List<Walk>> DeleteAllWalksAsync();
    }
}
