﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NzWalkAPI.Models.Domain;

namespace NzWalkAPI.Data
{
    public class NZWalksDbContext:DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }
        public DbSet<Difficulty> Difficulties  { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
         


    }
}
