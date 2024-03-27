﻿using System.ComponentModel.DataAnnotations;

namespace NzWalkAPI.Models.DTO
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
