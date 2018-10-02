using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Dapper.Contrib.Extensions;
using IISLogsToSqlServer.Models;
using PG.SqlBatchInsert;
using Z.Dapper.Plus;

namespace IISLogsToSqlServer.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly string _connectionString;
        private SqlWriter<T> _sqlWriter;

        public BaseRepository(IConnectionInfo connectionInfo)
        {
            _connectionString = connectionInfo.ConnectionString;

            _sqlWriter = new SqlWriter<T>();
        }

        private SqlConnection Connection => new SqlConnection(_connectionString);

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

            //foreach (var entity in entities)
            //{
            //    Add(entity);
            //}

            //return;
            var stopWatch = Stopwatch.StartNew();

            using (var connection = Connection)
            {
                connection.Open();

                //using (var transaction = connection.BeginTransaction())
                //{
                    //_sqlWriter.Write(connection, entities);
                //}

                connection.BulkInsert(entities);
            }

            stopWatch.Stop();

            Console.WriteLine(stopWatch.ElapsedMilliseconds);
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
    }
}
