using MetricsManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Xunit;

namespace ModelsTests.MetricsManagerTests
{
    public class AgentsFolderUnitTests
    {
        private AgentsFolder _folder;

        public AgentsFolderUnitTests()
        {
            _folder = new AgentsFolder();
        }

        [Fact]
        public void GetAgents_CheckGet()
        {
            for (int i = 0; i < 10; i++)
            {
                var agent = new AgentInfo();
                _folder.AddAgent(agent);
            }

            var agents = _folder.GetAgents();

            _ = Assert.IsAssignableFrom<List<AgentInfo>>(agents);
        }

        [Fact]
        public void AddAgent_CheckAdd()
        {
            var count = 5;
            for (int i = 0; i < count; i++)
            {
                var agent = new AgentInfo();
                _folder.AddAgent(agent);
            }

            var agentsCount = _folder.GetAgents().Count;

            Assert.Equal(count, agentsCount);
        }
    }
}
