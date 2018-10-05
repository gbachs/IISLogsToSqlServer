using System.Collections.Generic;
using System.Linq;
using Dapper;
using IISLogsToSqlServer.Common.Models;
using IISLogsToSqlServer.Common.Repositories.Interfaces;
using IISLogsToSqlServer.Parser.Models;
using Z.Dapper.Plus;

namespace IISLogsToSqlServer.Common.Repositories
{
    public class LogEventToProcessRepository : BaseRepository<LogEventToProcess>, ILogEventToProcessRepository
    {
        public LogEventToProcessRepository(IConnectionInfo connectionInfo)
            : base(connectionInfo)
        {
        }

        public List<LogEventToProcess> LoadTopNotProcessed(int top)
        {
            using (var connection = Connection)
            {
                connection.Open();
                var query = $"select top {top} * from RawLogsToProcess with (nolock)";
                return connection.Query<LogEventToProcess>(query, commandTimeout: 10000).ToList();
            }
        }

        public void BulkDelete(List<LogEventToProcess> work)
        {
            using (var connection = Connection)
            {
                connection.Open();
                connection.BulkDelete(work);
            }
        }
    }
}