using NzWalkAPI.Models.Domain;

namespace NzWalkAPI.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }


        //Navigation Properties

        //public DifficultyDto Diffuclty { get; set; }
        public RegionDto Region { get; set; }
    }
}

