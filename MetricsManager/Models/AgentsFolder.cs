using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Models
{
    public class AgentsFolder
    {

        private List<AgentInfo> _folder = new List<AgentInfo>();

        public void AddAgent(AgentInfo agent)
        {
            _folder.Add(agent);
        }


        public List<AgentInfo> GetAgents()
        {
            return _folder;
        }
    }
}
