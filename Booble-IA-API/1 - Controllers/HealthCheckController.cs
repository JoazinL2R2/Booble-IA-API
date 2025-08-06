using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Booble_IA_API._1___Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous] // Keep public for monitoring systems
        public IActionResult HealthCheck()
        {
            return Ok(new { 
                Status = "Healthy", 
                Timestamp = DateTime.UtcNow,
                Version = "1.0.0",
                Service = "Booble IA API"
            });
        }

        [HttpGet("detailed")]
        [Authorize] // Protected endpoint for detailed health info
        public IActionResult DetailedHealthCheck()
        {
            try
            {
                // Here you could add more detailed health checks
                // Database connectivity, external services, etc.
                return Ok(new { 
                    Status = "Healthy", 
                    Timestamp = DateTime.UtcNow,
                    Version = "1.0.0",
                    Service = "Booble IA API",
                    Database = "Connected", // Placeholder
                    Memory = "Normal", // Placeholder
                    Uptime = "Unknown" // Placeholder
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Status = "Unhealthy", 
                    Error = ex.Message,
                    Timestamp = DateTime.UtcNow
                });
            }
        }
    }
}
