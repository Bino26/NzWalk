using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NzWalkAPI.Data;
using NzWalkAPI.Models.Domain;

namespace NzWalkAPI.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NzWalkDbContext nzWalkDbContext;

        public WalkRepository(NzWalkDbContext nzWalkDbContext)
        {
            this.nzWalkDbContext = nzWalkDbContext;
        }
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await nzWalkDbContext.Walks.AddAsync(walk);
            await nzWalkDbContext.SaveChangesAsync();
            return walk;
            
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var walk = await nzWalkDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }
             nzWalkDbContext.Remove(walk);
            await nzWalkDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetWalkAsync()
        {
            return await nzWalkDbContext.Walks.ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            return await nzWalkDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            throw new NotImplementedException();
        }
    }
}
