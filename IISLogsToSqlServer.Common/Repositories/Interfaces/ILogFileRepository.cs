using System;
using System.Collections.Generic;
using IISLogsToSqlServer.Common.Models;

namespace IISLogsToSqlServer.Common.Repositories.Interfaces
{
    public interface ILogFileRepository : IRepository<LogFile>
    {
        List<LogFile> GetAllForServer(Guid serverId);
    }
}