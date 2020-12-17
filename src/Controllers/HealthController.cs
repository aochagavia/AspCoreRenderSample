using Microsoft.AspNetCore.Mvc;

namespace AspCoreK8sSample.Controllers
{
    public class HealthController : Controller
    {
        public IActionResult Index()
        {
            return NoContent();
        }

        // Configure this endpoint as liveness check to see how kubernetes restarts
        // your pod
        public IActionResult Fail()
        {
            return BadRequest();
        }
    }
}
