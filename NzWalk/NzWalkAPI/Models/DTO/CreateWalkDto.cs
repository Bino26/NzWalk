using System.ComponentModel.DataAnnotations;

namespace NzWalkAPI.Models.DTO
{
    public class CreateWalkDto
    {
        [Required]
        [StringLength(100,MinimumLength =3,ErrorMessage ="Must contains a minimum of 3 characters and a maximum of 100 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Must contains a minimum of 3 characters and a maximum of 100 characters")]
        public string Description { get; set; }
        [Required]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
