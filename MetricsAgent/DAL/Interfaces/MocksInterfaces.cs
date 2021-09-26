using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Interfaces
{
    //Интерфейсы для заглушек;
    public interface ICpuMetricsRepository : IRepository<BaseMetricModel>
    {

    }
    public interface IDotNetMetricsRepository : IRepository<BaseMetricModel>
    {

    }

    public interface IHddMetricsRepository : IRepository<BaseMetricModel>
    {

    }

    public interface INetworkMetricsRepository : IRepository<BaseMetricModel>
    {

    }

    public interface IRamMetricsRepository : IRepository<BaseMetricModel>
    {

    }
}
