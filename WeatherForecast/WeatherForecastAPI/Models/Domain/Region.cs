﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NzWalkAPI.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description {  get; set; }
        public double LengthInKm {  get; set; }
        public string? RegionImageUrl {  get; set; }
    }
}
