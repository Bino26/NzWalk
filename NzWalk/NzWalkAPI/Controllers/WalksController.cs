using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalkAPI.CustomActionFilters;
using NzWalkAPI.Models.Domain;
using NzWalkAPI.Models.DTO;
using NzWalkAPI.Repositories;
using System.Text.Json;

namespace NzWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        private readonly ILogger<WalksController> loggerwalks;

        public WalksController(IWalkRepository walkRepository,IMapper mapper,ILogger<WalksController>loggerwalks)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.loggerwalks = loggerwalks;
        }
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> CreateWalk([FromBody] CreateWalkDto createWalkDto)
        {
            var walkDomainModel = mapper.Map<Walk>(createWalkDto);
            await walkRepository.CreateWalkAsync(walkDomainModel);
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return CreatedAtAction(nameof(GetWalkById), new { id = walkDto.Id }, walkDto);

        }
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> GetWalkById([FromRoute]Guid id)
        {
            var walkDomainModel = await walkRepository.GetWalkByIdAsync(id);
            if (walkDomainModel is null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }
        [HttpGet]
        // GET: /api/walks?filterOn=Name&filterQuery=Track&isAscending=true
        [Authorize(Roles = "Writer,Reader")]

        public async Task<IActionResult> GetWalk([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery]bool?isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            loggerwalks.LogInformation("GetWalk Method has been invoked");
            var walks = await walkRepository.GetWalkAsync(filterOn,filterQuery,sortBy,isAscending??true, pageNumber,pageSize);
            loggerwalks.LogInformation($"Finished GetWalk Request with data : {JsonSerializer.Serialize(walks)}");
            return Ok(mapper.Map<List<WalkDto>>(walks));

        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> DeleteWalk([FromRoute]Guid id)
        {
            var walk = await walkRepository.DeleteWalkAsync(id);
            return NoContent();
        }
       
        [HttpDelete]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> DeleteAllRegions()
        {
            var regionDomainModel = await walkRepository.DeleteAllWalksAsync();
            var message = new
            {
                status = true,
                message = "All walks has been deleted"
            };
            return new JsonResult(message);

        }
        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateWalk([FromRoute]Guid id, [FromBody] UpdateWalkDto updateWalkDto)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkDto);
             var result = await walkRepository.UpdateWalkAsync(id, walkDomainModel);
            
            return Ok(result);
        }
    }
}
