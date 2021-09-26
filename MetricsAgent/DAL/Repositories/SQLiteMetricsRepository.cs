using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace MetricsAgent.DAL
{
    public abstract class SQLiteMetricsRepository : ICpuMetricsRepository, IDotNetMetricsRepository, IHddMetricsRepository, INetworkMetricsRepository, IRamMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        protected string _tableName;

        public void Create(BaseMetricModel item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute($"INSERT INTO {_tableName}(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
            }

        }


        public void Update(BaseMetricModel item)
        {

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute($"UPDATE {_tableName} SET value = @value, time = @time WHERE id=@id;",
                    new
                    {
                        value = item.Value,
                        time = item.Time,
                        id = item.Id
                    });
            }

        }


        public void Delete(int id)
        {

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute($"DELETE FROM {_tableName} WHERE id=@id;", new { id = id });
            }

        }


        public BaseMetricModel GetByIdMetrics(int id)
        {

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<BaseMetricModel>($"SELECT * FROM {_tableName} WHERE id=@id;", new { id = id });
            }

        }


        public IList<BaseMetricModel> GetInRangeMetrics(DateTime fromTime, DateTime toTime)
        {
            //Конвертируем в необходимый формат;
            string convertFromTime = fromTime.ToString("yyyy-MM-dd HH:mm:ss.fffZ");
            string converttoTime = toTime.ToString("yyyy-MM-dd HH:mm:ss.fffZ");

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<BaseMetricModel>($"SELECT * FROM {_tableName} WHERE time BETWEEN '{convertFromTime}' AND '{converttoTime}'").ToList();
            }
        }


        public IList<BaseMetricModel> GetAllMetrics()
        {

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<BaseMetricModel>($"SELECT * FROM {_tableName};").ToList();
            }

        }
    }
}
