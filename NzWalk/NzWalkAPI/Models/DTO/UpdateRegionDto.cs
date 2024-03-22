using System.ComponentModel.DataAnnotations;

namespace NzWalkAPI.Models.DTO
{
    public class UpdateRegionDto
    {

        [Required]
        [MinLength(3, ErrorMessage = "Name must contains a minimu of 3 characters")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Code must contains a minimu of 3 characters")]
        public string Code { get; set; }
        [Required]
        [Range(0,50)]
        public double LengthInKm { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
