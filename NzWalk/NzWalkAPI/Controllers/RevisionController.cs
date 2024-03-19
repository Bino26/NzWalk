using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalkAPI.Data;

namespace NzWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevisionController : ControllerBase
    {
        private readonly NzWalkDbContext dbContext;

        public RevisionController(NzWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll() {
            var regions = dbContext.Regions.ToList();
            return Ok(regions);
        
        }
        [Route("{id:Guid}")]
        [HttpGet]

        public IActionResult GetById([FromRoute] Guid id) {
            var region = dbContext.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        
        }
       
    }
}
