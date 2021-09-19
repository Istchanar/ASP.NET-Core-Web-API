using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке

    public class RamMetricsRepository : SQLiteMetricsRepository
    {
        public RamMetricsRepository() 
        {
            _tableName = "rammetrics";
        }
    }
}
