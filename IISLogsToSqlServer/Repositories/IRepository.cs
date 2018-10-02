using System;
using System.Collections.Generic;

namespace IISLogsToSqlServer.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetByKey(Guid entityId);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        void BulkAdd(List<T> entities);
    }
}