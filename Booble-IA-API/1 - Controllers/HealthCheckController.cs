using Microsoft.AspNetCore.Mvc;

namespace Booble_IA_API._1___Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult HealthCheck()
        {
            return Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow });
        }
    }

}
