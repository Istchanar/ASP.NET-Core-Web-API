using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Models
{
    public class BaseMetricModel
    {
        public DateTime Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
 
    }
}
