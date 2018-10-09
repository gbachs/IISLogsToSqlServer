using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using IISLogsToSqlServer.Common.Models;
using IISLogsToSqlServer.Common.Repositories.Interfaces;

namespace IISLogsToSqlServer.Common.Repositories
{
    public class LogFileRepository : BaseRepository<LogFile>, ILogFileRepository
    {
        public LogFileRepository(IConnectionInfo connectionInfo)
            : base(connectionInfo)
        {
        }

        public List<LogFile> GetAllForServer(Guid serverId)
        {
            using (var connection = Connection)
            {
                connection.Open();
                var query = $"select * from LogFiles where ServerId = @ServerId";

                return connection.Query<LogFile>(query, new { ServerId = serverId }).ToList();
            }
        }
    }
}