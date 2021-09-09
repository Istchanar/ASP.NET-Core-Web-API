using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.Models;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private AgentsFolder _agentsFolder;

        private readonly ILogger<CpuMetricsController> _logger;
        public AgentsController(AgentsFolder agentsFolder, ILogger<CpuMetricsController> logger)
        {
            _agentsFolder = agentsFolder;
            _logger = logger;
            _logger.LogDebug(1, "Added agent list.");
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation("Registrated new agent: " + agentInfo.AgentId);
            _agentsFolder.AddAgent(agentInfo);
            return Ok();
        }

        [HttpGet("register/list")]
        public IActionResult GetAgentsList()
        {
            _logger.LogInformation("Returned agent list.");
            return Ok(_agentsFolder.GetAgents());
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("Agent enable: " + agentId);
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("Agent disable: " + agentId);
            return Ok();
        }
    }
}
