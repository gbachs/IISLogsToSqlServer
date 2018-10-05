using System.Collections.Generic;

namespace IISLogsToSqlServer.Common.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        void Add(T entity);
        void UpdateAll(List<T> entities);
        void BulkAdd(List<T> entities);
    }
}