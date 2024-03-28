using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NzWalkAPI.Data;
using NzWalkAPI.Models.Domain;
using System.Globalization;

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

        public async Task<List<Walk>> DeleteAllWalksAsync()
        {
            var walks = await nzWalkDbContext.Walks.ToListAsync();
            nzWalkDbContext.Walks.RemoveRange(walks);
            return null;
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

        public async Task<List<Walk>> GetWalkAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null,bool isAscending = true)
        {
            var walks = nzWalkDbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            
            //Filtering

            if ((string.IsNullOrWhiteSpace(filterOn) == false) && (string.IsNullOrWhiteSpace(filterQuery) == false))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }
            
            // Sorting 
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            return  await walks.ToListAsync();   


               
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            return await nzWalkDbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalk = await nzWalkDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.RegionId = walk.RegionId;

            await nzWalkDbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
