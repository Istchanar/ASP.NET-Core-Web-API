using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {

        void Create(T item);

        void Update(T item);

        void Delete(int id);

        T GetByIdMetrics(int id);

        IList<T> GetInRangeMetrics(DateTime fromTime, DateTime toTime);

        IList<T> GetAllMetrics();
    }
}
