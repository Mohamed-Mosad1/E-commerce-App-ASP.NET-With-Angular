using E_commerce.API.Errors;
using E_commerce.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public BuggyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            var prod = _dbContext.Products.Find(50);
            if (prod == null)
            {
                return NotFound(new BaseCommonResponse(404));
            }
            return Ok(prod);
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            var prod = _dbContext.Products.Find(50);
            prod.Name = "";
            return Ok(prod);
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            return Ok();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new BaseCommonResponse(400));
        }
    }
}
