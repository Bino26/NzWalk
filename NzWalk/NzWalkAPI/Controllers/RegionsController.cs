using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalkAPI.Data;
using NzWalkAPI.Models.Domain;

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
            var regions = dbContext.Regions.ToList();   
            
            return Ok(regions);


        }
        //Get RegionById
        [Route("{id:Guid}")]
        [HttpGet]
        public IActionResult GetById([FromRoute]Guid id) { 
            var region = dbContext.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        
        }
    }
}
