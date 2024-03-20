using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NzWalkAPI.Data;
using NzWalkAPI.Models.Domain;
using NzWalkAPI.Models.DTO;
using NzWalkAPI.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace NzWalkAPI.Controllers
{
    [Route("api/regions")]
    //[Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase

        //Get All Regions
    {

        private readonly IRegionRepository regionRepository;

        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from Database - AppDomain Models
            var regions = await regionRepository.GetAllAsync();

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
            var region = await regionRepository.GetByIdAsync(id);
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

            await regionRepository.CreateRegionAsync(regionDomainModel);
           

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
            
            //Dto from Domain Model
            var regionDomainModel = new Region
            {
                Code = updateRegionDto.Code,
                Name = updateRegionDto.Name,
                LengthInKm = updateRegionDto.LengthInKm,
                RegionImageUrl = updateRegionDto.RegionImageUrl


            };
            //Check if id exists in the database
            await regionRepository.UpdateRegionAsync(id,regionDomainModel);

            var regionDto = new RegionDto
            {
                
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
            var regionDomainModel = await regionRepository.DeleteRegionAsync(id);
            return NoContent();
        }
        
        
    }
}
