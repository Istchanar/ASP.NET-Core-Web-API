using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using Microsoft.Extensions.Logging;
using MetricsAgent.Models;

namespace MetricsAgent.Controllers
{
    [Route("metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly IDotNetMetricsRepository _repository;

        private readonly ILogger<DotNetMetricsController> _logger;

        public DotNetMetricsController(IDotNetMetricsRepository repository, ILogger<DotNetMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "Agent DotNetMetricsController activated.");
        }

        [HttpPost("create")]
        public IActionResult MetricCreate([FromBody] MetricCreateRequest request)
        {
            _repository.Create(new MetricDto
            {
                Time = request.Time,
                Value = request.Value
            });

            _logger.LogInformation($"DOTNET MetricCreate request successful: value {request.Value}, create time {request.Time}");
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult MetricsGetAll()
        {
            var response = new MetricCreateResponse()
            {
                Metrics = (List<MetricDto>)_repository.GetAllMetrics()
            };
            var observation = response.GetObservations();

            _logger.LogInformation($"DOTNET MetricGetAll request successful. {observation} observations total.");
            return Ok(response);
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult MetricsGetInRange([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            var response = new MetricCreateResponse()
            {
                Metrics = (List<MetricDto>)_repository.GetInRangeMetrics(fromTime, toTime)
            };
            var observation = response.GetObservations();

            _logger.LogInformation($"DOTNET MetricsGetInRange request successful. {observation} observations total from {fromTime} to {toTime}.");
            return Ok(response);
        }
    }
}