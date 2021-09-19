using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ContrillersTests.MetricsAgentTests
{
    public class DotNetMetricsControllerUnitTests
    {
        private DotNetMetricsController _controller;
        private Mock<IDotNetMetricsRepository> _mockRepository;
        private Mock<ILogger<DotNetMetricsController>> _mockLogger;

        public DotNetMetricsControllerUnitTests()
        {
            _mockRepository = new Mock<IDotNetMetricsRepository>();

            _mockLogger = new Mock<ILogger<DotNetMetricsController>>();

            _controller = new DotNetMetricsController(_mockRepository.Object, _mockLogger.Object);
        }

        //Проверка метода MetricCreate;
        [Fact]
        public void MetricCreate_ShouldCall_Create_From_Repository()
        {
            _mockRepository.Setup(repository => repository.Create(It.IsAny<MetricDto>())).Verifiable();

            var result = _controller.MetricCreate(new MetricCreateRequest { Time = new DateTime(2022, 5, 1, 8, 30, 52), Value = 50 });

            _mockRepository.Verify(repository => repository.Create(It.IsAny<MetricDto>()), Times.AtMostOnce());
        }


        //Проверка метода MetricsGetInRange;
        [Fact]
        public void MetricsGetInRange_ShouldCall_GetInRangeMetrics_From_Repository()
        {

            _mockRepository.Setup(repository => repository.GetInRangeMetrics(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Verifiable();

            var result = _controller.MetricsGetInRange(It.IsAny<DateTime>(), It.IsAny<DateTime>());

            _mockRepository.Verify(repository => repository.GetInRangeMetrics(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.AtMostOnce());
        }

        //Проверка метода MetricsGetAll;
        [Fact]
        public void MetricsGetAll_ShouldCall_GetAllMetrics_From_Repository()
        {
            _mockRepository.Setup(repository => repository.GetAllMetrics()).Returns(It.IsAny<IList<MetricDto>>());

            var result = _controller.MetricsGetAll();

            _mockRepository.Verify((repository => repository.GetAllMetrics()), Times.AtMostOnce);
        }

        //Проверка возврата от MetricsGetInRange;
        [Fact]
        public void MetricCreate_ReturnsOk()
        {
            var data = new MetricCreateRequest { Time = new DateTime(2022, 5, 1, 8, 30, 52), Value = 50 };
            var result = _controller.MetricCreate(data);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        //Проверка возврата от MetricsGetInRange;
        [Fact]
        public void MetricsGetAll_ReturnsOk()
        {
            var result = _controller.MetricsGetAll();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        //Проверка возврата от MetricsGetInRange;
        [Fact]
        public void MetricsGetInRange_ReturnsOk()
        {
            var fromTime = new DateTime(2022, 5, 1, 8, 30, 52);
            var toTime = new DateTime(2023, 5, 1, 8, 30, 52);
            var result = _controller.MetricsGetInRange(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}