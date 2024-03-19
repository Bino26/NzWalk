using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalkAPI.Data;
using NzWalkAPI.Models.Domain;
using NzWalkAPI.Models.DTO;
using System.Reflection.Metadata.Ecma335;

namespace NzWalkAPI.Controllers
{
    [Route("api/regions")]
    //[Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase

        //Get All Regions
    {
        private readonly NzWalkDbContext dbContext;

        public RegionsController(NzWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //Get Data from Database - AppDomain Models
            var regions = dbContext.Regions.ToList();

            //Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                //use domain model to add
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }
            
            return Ok(regionsDto);


        }
        //Get RegionById
        [Route("{id:Guid}")]
        [HttpGet]
        public IActionResult GetById([FromRoute]Guid id) {

            //Get Region from Database - AppDomain Models
            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            //Map/Convert Region Domain Lodels to Region Dto

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };
            return Ok(regionDto);
        
        }
        //Create Region
        [HttpPost]
        public IActionResult CreateRegion([FromBody] AddRegionDto addRegionDto)
        {
            //Map from Dto to Domain Model
            var regionDomainModel = new Region
            {
                Name = addRegionDto.Name,
                Code = addRegionDto.Code,
                RegionImageUrl = addRegionDto.RegionImageUrl,
            };
           
            //Create region to the database

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //Map from Domain Model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name=regionDomainModel.Name,
                Code=regionDomainModel.Code,
                RegionImageUrl=regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById),new { id=regionDto.Id },regionDto);
        }

        
    }
}
