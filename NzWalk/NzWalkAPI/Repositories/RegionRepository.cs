using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NzWalkAPI.Data;
using NzWalkAPI.Models.Domain;

namespace NzWalkAPI.Repositories
{
    public class RegionRepository:IRegionRepository
    {
        private readonly NzWalkDbContext nzWalkDbContext;

        public RegionRepository(NzWalkDbContext nzWalkDbContext)
        {
            this.nzWalkDbContext = nzWalkDbContext;
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await nzWalkDbContext.Regions.AddAsync(region);
            await nzWalkDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var existingRegion = await nzWalkDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(existingRegion == null)
            {
                return null;
            }
             nzWalkDbContext.Regions.Remove(existingRegion);
            await nzWalkDbContext.SaveChangesAsync();
            return existingRegion;

        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await nzWalkDbContext.Regions.ToListAsync();

        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await nzWalkDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateRegionAsync(Guid id , Region region)
        {
            var existingRegion =await nzWalkDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
           
            existingRegion.LengthInKm = region.LengthInKm;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            existingRegion.Code = region.Code;
            existingRegion.Name=region.Name;

            await nzWalkDbContext.SaveChangesAsync();
            return existingRegion;

        }
    }
}
