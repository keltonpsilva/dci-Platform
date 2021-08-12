using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            var response = new HomeControllerModel { Application = "Web Api", Documentation = $"{Request.Scheme}://{Request.Host}/docs" };

            return Ok(response);
        }
    }
}
