using ElasticSearchHelper.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DragonCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElasticController : ControllerBase
    {
        private readonly IElasticSearchService _elasticClient;

        public ElasticController(IElasticSearchService elasticClient)
        {
            _elasticClient = elasticClient;
        }

        [HttpGet]
        [Route("PingElastic")]
        public async Task<IActionResult> PingElastic()
        {
            try
            {
                var response = await _elasticClient.TestConnectionAsync();

                return response.Success ? Ok(response.DebugInformation) : BadRequest(response.OriginalException);
            }
            catch (Exception ex)
            {
                return BadRequest("Error Occured: " + ex);
            }

        }
    }
}
