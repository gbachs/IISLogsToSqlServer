using System.Collections.Generic;
using IISLogsToSqlServer.Parser.Models;

namespace IISLogsToSqlServer.Common.Repositories.Interfaces
{
    public interface ILogEventToProcessRepository : IRepository<LogEventToProcess>
    {
        List<LogEventToProcess> LoadTopNotProcessed(int top);
        void BulkDelete(List<LogEventToProcess> work);
    }
}