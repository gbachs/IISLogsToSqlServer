using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper.Contrib.Extensions;
using IISLogsToSqlServer.Common.Models;
using IISLogsToSqlServer.Common.Repositories.Interfaces;
using Z.Dapper.Plus;

namespace IISLogsToSqlServer.Common.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly string _connectionString;

        public BaseRepository(IConnectionInfo connectionInfo)
        {
            _connectionString = connectionInfo.ConnectionString;
        }

        public SqlConnection Connection => new SqlConnection(_connectionString);

        public void Add(T entity)
        {
            using (var connection = Connection)
            {
                connection.Open();
                connection.Insert(entity);
            }
        }

        public void BulkAdd(List<T> entities)
        {
            using (var connection = Connection)
            {
                connection.Open();
                connection.BulkInsert(entities);
            }
        }

        public List<T> GetAll()
        {
            using (var connection = Connection)
            {
                connection.Open();
                return connection.GetAll<T>().ToList();
            }
        }

        public T GetByKey(Guid entityKey)
        {
            using (var connection = Connection)
            {
                connection.Open();
                return connection.Get<T>(entityKey);
            }
        }

        public void Delete(T entity)
        {
            using (var connection = Connection)
            {
                connection.Open();
                connection.Delete(entity);
            }
        }

        public void Update(T entity)
        {
            using (var connection = Connection)
            {
                connection.Open();
                connection.Update(entity);
            }
        }

        public void UpdateAll(List<T> entities)
        {
            using (var connection = Connection)
            {
                connection.Open();
                connection.BulkUpdate(entities);
            }
        }
    }
}
