using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ObservabilitySampleApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this._logger = logger;
        }
        
        // GET api/error
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("About to throw an error");
            
            throw new Exception("Something has gone horribly wrong");
        }
    }
}