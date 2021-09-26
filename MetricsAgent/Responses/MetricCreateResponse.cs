using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Responses
{
    public class MetricCreateResponse
    {
        public List<MetricDto> Metrics { get; set; }

        public int GetObservations()
        {
            if (Metrics!=null)
            {
                return Metrics.Count;
            }
            return 0;
        }
    }
}
