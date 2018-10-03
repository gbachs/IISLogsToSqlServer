using System;
using System.Collections.Generic;

namespace IISLogsToSqlServer.Common.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetByKey(Guid entityId);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        void UpdateAll(List<T> entities);
        void BulkAdd(List<T> entities);
    }
}