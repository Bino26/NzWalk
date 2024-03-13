using Microsoft.EntityFrameworkCore;
using NzWalkAPI.Models.Domain;
using System.Collections.Generic;

namespace NzWalkAPI.Data
{
    public class NzWalkDbContext:DbContext
    {
        public NzWalkDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }



    }
    
}