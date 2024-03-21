using AutoMapper;
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
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from Database - AppDomain Models
            var regions = await regionRepository.GetAllAsync();

            //Map Domain Models to DTOs
            var regionsDto = mapper.Map<List<RegionDto>>(regions);
            
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

            var regionDto = mapper.Map< RegionDto > (region);
            return Ok(regionDto);
        
        }
        //Create Region
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionDto addRegionDto)
        {
            //Map from Dto to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionDto);
            //Create region to the database

            await regionRepository.CreateRegionAsync(regionDomainModel);


            //Map from Domain Model to Dto
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById),new { id=regionDto.Id },regionDto);
        }

        //Update Region

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {

            //Dto from Domain Model

            //Manuel Mapping 

            //var regionDomainModel = new Region
            //{
            //    Code = updateRegionDto.Code,
            //    Name = updateRegionDto.Name,
            //    LengthInKm = updateRegionDto.LengthInKm,
            //    RegionImageUrl = updateRegionDto.RegionImageUrl


            //};

            var regionDomainModel = mapper.Map<Region>(updateRegionDto);
            //Check if id exists in the database
            await regionRepository.UpdateRegionAsync(id,regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
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
