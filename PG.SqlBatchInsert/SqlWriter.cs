using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PG.SqlBatchInsert.Interfaces;

namespace PG.SqlBatchInsert
{
    public class SqlWriter<T> : ISqlWriter<T>
    {
        private readonly SqlMetadata<T> _metadata;

        public SqlWriter()
        {
            _metadata = new SqlMetadata<T>();
        }
     
        public void Write(IDbTransaction transaction, List<T> items)
        {
            if (items == null || !items.Any())
                return;

            BulkSaveItems(transaction, items);
        }

        private void BulkSaveItems(IDbTransaction transaction, IEnumerable<T> list)
        {
            var sqlBulkCopy = CreateSqlBulkCopy((SqlTransaction) transaction);
            using (var dataReader = new SqlMetadataReader<T>(_metadata.Properties, list))
            {
                sqlBulkCopy.WriteToServer(dataReader);
            }
        }

        private SqlBulkCopy CreateSqlBulkCopy(SqlTransaction transaction)
        {
            const SqlBulkCopyOptions options = SqlBulkCopyOptions.Default | SqlBulkCopyOptions.TableLock;

            var sqlBulkCopy = new SqlBulkCopy(transaction.Connection, options, transaction)
            {
                DestinationTableName = _metadata.TableName,
                BulkCopyTimeout = 500000
            };

            AddBulkCopyMappings(sqlBulkCopy);

            return sqlBulkCopy;
        }

        private void AddBulkCopyMappings(SqlBulkCopy sqlBulkCopy)
        {
            _metadata.Properties.ForEach(x => sqlBulkCopy.ColumnMappings.Add(x.PropertyName, x.ColumnName));
        }

        public void Dispose()
        {
        }

    }
}