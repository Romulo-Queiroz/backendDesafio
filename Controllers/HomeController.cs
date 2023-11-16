using Microsoft.AspNetCore.Mvc;

namespace Projeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("The API is running.");
        }
    }
}
