using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ObservabilitySampleApp.WebApi.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Home controller hit");

            return new OkResult();
        }
    }
}