using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.API.Errors;
using Product.Infrastructure.Data;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BugController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            var products = _context.Products.Find(50);
            if (products == null)
            {
                return NotFound(new BaseCommonResponse(404));
            }
            return Ok(products);
        }

        [HttpGet("server-error")]
        public ActionResult GetServerError() {
            var product = _context.Products.Find(100);
            product.Name = "";
            return Ok();
        }

        [HttpGet("bad-request/{id}")]
        public ActionResult GetNotFoundById()
        {
            return Ok();
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }
    }
}
