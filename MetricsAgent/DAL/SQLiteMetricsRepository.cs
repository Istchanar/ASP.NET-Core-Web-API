using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public abstract class SQLiteMetricsRepository : ICpuMetricsRepository, IDotNetMetricsRepository, IHddMetricsRepository, INetworkMetricsRepository, IRamMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        protected string _tableName;

        public void Create(MetricDto item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);

            command.CommandText = $"INSERT INTO {_tableName}(value, time) VALUES(@value, @time)";
            command.Parameters.AddWithValue("@value", item.Value);
            command.Parameters.AddWithValue("@time", item.Time);
            command.Prepare();
            command.ExecuteNonQuery();
        }


        public void Update(MetricDto item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);

            command.CommandText = $"UPDATE {_tableName} SET value = @value, time = @time WHERE id=@id;";
            command.Parameters.AddWithValue("@id", item.Id);
            command.Parameters.AddWithValue("@value", item.Value);
            command.Parameters.AddWithValue("@time", item.Time);
            command.Prepare();
            command.ExecuteNonQuery();
        }


        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);

            command.CommandText = $"DELETE FROM {_tableName} WHERE id=@id;";
            command.Parameters.AddWithValue("@id", id);
            command.Prepare();
            command.ExecuteNonQuery();
        }


        public MetricDto GetByIdMetrics(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
           
            using var command = new SQLiteCommand(connection);
            command.CommandText = $"SELECT * FROM {_tableName} WHERE id=@id;";

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new MetricDto
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetDateTime(2)
                    };
                }
                else
                {
                    return null;
                }
            }
        }


        public IList<MetricDto> GetInRangeMetrics(DateTime fromTime, DateTime toTime)
        {
            //Конвертируем в необходимый формат;
            string convertFromTime = fromTime.ToString("yyyy-MM-dd HH:mm:ss.fffZ");
            string converttoTime = toTime.ToString("yyyy-MM-dd HH:mm:ss.fffZ");

            using var conection = new SQLiteConnection(ConnectionString);
            conection.Open();

            using var command = new SQLiteCommand(conection);
            command.CommandText = $"SELECT * FROM {_tableName} WHERE time BETWEEN '{convertFromTime}' AND '{converttoTime}'";

            var dataList = new List<MetricDto>();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataList.Add(new MetricDto
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetDateTime(2)
                    });
                }
            }
            return dataList;
        }


        public IList<MetricDto> GetAllMetrics()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);
            command.CommandText = $"SELECT * FROM {_tableName};";

            var dataList = new List<MetricDto>();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataList.Add(new MetricDto
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetDateTime(2)
                    });
                }
            }
            return dataList;
        }
    }
}
