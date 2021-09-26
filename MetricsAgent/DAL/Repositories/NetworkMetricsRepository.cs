using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Repositories
{
    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке

    public class NetworkMetricsRepository : SQLiteMetricsRepository
    {
        public NetworkMetricsRepository() 
        {
            _tableName = "networkmetrics";
        }
    }
}
