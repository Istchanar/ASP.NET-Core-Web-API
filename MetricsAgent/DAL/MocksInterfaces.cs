using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    //Интерфейсы для заглушек;
    public interface ICpuMetricsRepository : IRepository<MetricDto>
    {

    }
    public interface IDotNetMetricsRepository : IRepository<MetricDto>
    {

    }

    public interface IHddMetricsRepository : IRepository<MetricDto>
    {

    }

    public interface INetworkMetricsRepository : IRepository<MetricDto>
    {

    }

    public interface IRamMetricsRepository : IRepository<MetricDto>
    {

    }
}
