using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace MetricsAgent.Controllers
{
    [Route("metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly IRamMetricsRepository _repository;

        private readonly IMapper _mapper;

        private readonly ILogger<RamMetricsController> _logger;

        public RamMetricsController(IRamMetricsRepository repository, IMapper mapper, ILogger<RamMetricsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "Agent RamMetricsController activated.");
        }

        [HttpPost("create")]
        public IActionResult MetricCreate([FromBody] MetricCreateRequest request)
        {
            _repository.Create(new BaseMetricModel
            {
                Time = request.Time,
                Value = request.Value
            });

            _logger.LogInformation($"RAM MetricCreate request successful: value {request.Value}, create time {request.Time}");
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult MetricsGetAll()
        {
            var response = new MetricCreateResponse()
            {
                Metrics = new List<MetricDto>()
            };

            try
            {
                IList<BaseMetricModel> metrics = _repository.GetAllMetrics();

                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<MetricDto>(metric));
                }
            }
            catch (Exception error)
            {
                _logger.LogInformation(error.ToString());
            }

            var observation = response.GetObservations();

            _logger.LogInformation($"RAM MetricGetAll request successful. {observation} observations total.");
            return Ok(response);
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult MetricsGetInRange([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {

            var response = new MetricCreateResponse()
            {
                Metrics = new List<MetricDto>()
            };

            try
            {
                IList<BaseMetricModel> metrics = _repository.GetInRangeMetrics(fromTime, toTime);

                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<MetricDto>(metric));
                }
            }
            catch (Exception error)
            {
                _logger.LogInformation(error.ToString());
            }

            var observation = response.GetObservations();

            _logger.LogInformation($"RAM MetricsGetInRange request successful. {observation} observations total from {fromTime} to {toTime}.");
            return Ok(response);
        }
    }
}
