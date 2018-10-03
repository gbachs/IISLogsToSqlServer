using System.Collections.Generic;
using IISLogsToSqlServer.Parser.Models;

namespace IISLogsToSqlServer.Common.Repositories.Interfaces
{
    public interface ILogEventRepository : IRepository<LogEvent>
    {
        List<LogEvent> LoadTopNotProcessed(int top);
    }
}