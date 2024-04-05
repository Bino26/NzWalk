using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalkAPI.CustomActionFilters;
using NzWalkAPI.Models.Domain;
using NzWalkAPI.Models.DTO;
using NzWalkAPI.Repositories;

namespace NzWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository,IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] CreateWalkDto createWalkDto)
        {
            var walkDomainModel = mapper.Map<Walk>(createWalkDto);
            await walkRepository.CreateWalkAsync(walkDomainModel);
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return CreatedAtAction(nameof(GetWalkById), new { id = walkDto.Id }, walkDto);

        }
        [HttpGet]
        [Route("{id}")]
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

        public async Task<IActionResult> GetWalk([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery]bool?isAscending)
        {
            var walks = await walkRepository.GetWalkAsync(filterOn,filterQuery,sortBy,isAscending??true);
            return Ok(mapper.Map<List<WalkDto>>(walks));

        }
        [HttpDelete]
        [Route("{id}")]
        
        public async Task<IActionResult> DeleteWalk([FromRoute]Guid id)
        {
            var walk = await walkRepository.DeleteWalkAsync(id);
            return NoContent();
        }
       
        [HttpDelete]

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
        public async Task<IActionResult> UpdateWalk([FromRoute]Guid id, [FromBody] UpdateWalkDto updateWalkDto)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkDto);
             await walkRepository.UpdateWalkAsync(id, walkDomainModel);
            
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
    }
}
