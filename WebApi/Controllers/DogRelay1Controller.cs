using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ObservabilitySampleApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogRelay1Controller : ControllerBase
    {
        private readonly ILogger<DogRelay1Controller> _logger;

        public DogRelay1Controller(ILogger<DogRelay1Controller> logger)
        {
            this._logger = logger;
        }
        
        // GET api/dog
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Getting a dog image from API running on k8s via relay 1..");

            SleepForLessThanASecond();
            
            using (HttpClient client = new HttpClient())
            {
                // https://dog.ceo/dog-api/
                var response = await client.GetAsync("http://observability-sample-service:4000/api/dog");
                
                _logger.LogInformation($"Dog image response from API running on k8s via relay 1 {response}", response);
                
                response.EnsureSuccessStatusCode();
                
                var stringResult = await response.Content.ReadAsStringAsync();
                return new JsonResult(stringResult);
            }
        }

        private void SleepForLessThanASecond()
        {
            Thread.Sleep(new Random().Next(200, 1000));
        }
    }
}