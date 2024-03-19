using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAll()
        {
            //Get Data from Database - AppDomain Models
            var regions = await dbContext.Regions.ToListAsync();

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
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id) {

            //Get Region from Database - AppDomain Models
            var region =  await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
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
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionDto addRegionDto)
        {
            //Map from Dto to Domain Model
            var regionDomainModel = new Region
            {
                Name = addRegionDto.Name,
                Code = addRegionDto.Code,
                RegionImageUrl = addRegionDto.RegionImageUrl,
            };
           
            //Create region to the database

            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

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

        //Update Region

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            //Check if id exists in the database
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            };

            //Dto from Domain Model
            regionDomainModel.Code = updateRegionDto.Code;
            regionDomainModel.Name = updateRegionDto.Name;
            regionDomainModel.LengthInKm = updateRegionDto.LengthInKm;
            regionDomainModel.RegionImageUrl = updateRegionDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            var regionDto = new UpdateRegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
                LengthInKm = regionDomainModel.LengthInKm,
            };
            return Ok(regionDto);
        }
        //Delete a region
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
               dbContext.Regions.Remove(regionDomainModel);
              await dbContext.SaveChangesAsync();
           
            return Ok();
        }
        
        
    }
}
