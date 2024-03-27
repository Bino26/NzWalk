using System.ComponentModel.DataAnnotations;

namespace NzWalkAPI.Models.DTO
{
    public class AddRegionDto
    {
        
        [Required]
        [MinLength(3, ErrorMessage = "Name must contains a minimum of 3 characters")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Description must contains a minimum of 3 characters")]
        public string   Description { get; set; }
        public double LengthInKm { get; set; }
        public string? RegionImageUrl { get; set; }
        
    }
}
