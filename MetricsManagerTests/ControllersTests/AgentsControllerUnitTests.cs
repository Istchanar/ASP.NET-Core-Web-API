using MetricsManager.Controllers;
using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace ControllersTests.MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController _controller;

        private Mock<ILogger<AgentsController>> _mockLogger;
        public AgentsControllerUnitTests()
        {
            var folder = new AgentsFolder();

            _mockLogger = new Mock<ILogger<AgentsController>>();

            _controller = new AgentsController(folder, _mockLogger.Object);
        }


        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            var agentInfo = new AgentInfo();

            var result = _controller.RegisterAgent(agentInfo);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetAgentsList_ReturnsOk()
        {
            var result = _controller.GetAgentsList();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            var agentId = 1;

            var result = _controller.EnableAgentById(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            var agentId = 1;

            var result = _controller.DisableAgentById(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
