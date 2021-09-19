using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Models
{
    public class MetricCreateRequest
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }

    }
}
