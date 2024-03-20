namespace NzWalkAPI.Models.DTO
{
    public class UpdateRegionDto
    {
       
        public string Name { get; set; }
        public string Code { get; set; }
        public double LengthInKm { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
