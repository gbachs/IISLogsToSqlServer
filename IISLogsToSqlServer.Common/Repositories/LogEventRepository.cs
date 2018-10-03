using System.Collections.Generic;
using System.Linq;
using Dapper;
using IISLogsToSqlServer.Common.Models;
using IISLogsToSqlServer.Common.Repositories.Interfaces;
using IISLogsToSqlServer.Parser.Models;

namespace IISLogsToSqlServer.Common.Repositories
{
    public class LogEventRepository : BaseRepository<LogEvent>, ILogEventRepository
    {
        public LogEventRepository(IConnectionInfo connectionInfo)
            : base(connectionInfo)
        {
        }

        public List<LogEvent> LoadTopNotProcessed(int top)
        {
            using (var connection = Connection)
            {
                connection.Open();
                var query = $"select top {top}* from RawLogs where Processed = 0";
                return connection.Query<LogEvent>(query).ToList();
            }
        }
    }
}