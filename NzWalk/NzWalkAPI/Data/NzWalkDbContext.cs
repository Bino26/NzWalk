using Microsoft.EntityFrameworkCore;
using NzWalkAPI.Models.Domain;
using System.Collections.Generic;

namespace NzWalkAPI.Data
{
    public class NzWalkDbContext:DbContext
    {
        public NzWalkDbContext(DbContextOptions<NzWalkDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seeding Data

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.NewGuid(),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.NewGuid(),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.NewGuid(),
                    Name = "Hard"
                },

            };

            
            modelBuilder.Entity<Difficulty>().HasData(difficulties);
        }



    }
    
}