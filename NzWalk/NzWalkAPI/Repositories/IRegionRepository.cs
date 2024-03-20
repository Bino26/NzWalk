﻿using NzWalkAPI.Models.Domain;

namespace NzWalkAPI.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region> GetByIdAsync(Guid id);
        Task<Region> CreateRegionAsync(Region region);
        Task<Region> UpdateRegionAsync(Guid id , Region region);
        Task<Region> DeleteRegionAsync(Guid id);

    }
}
