using System.ComponentModel.DataAnnotations;

namespace NzWalkAPI.Models.DTO
{
    public class UpdateRegionDto
    {

        [Required]
        [MinLength(3, ErrorMessage = "Name must contains a minimum of 3 characters")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Description must contains a minimumof 3 characters")]
        public string Description { get; set; }
        [Required]
        public double LengthInKm { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
