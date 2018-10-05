using System.Collections.Generic;
using System.IO;
using IISLogsToSqlServer.Parser.Models;

namespace IISLogsToSqlServer.Parser.Interfaces
{
    // ReSharper disable once InconsistentNaming
    public interface IIISEventLogReader
    {
        IEnumerable<LogEvent> Read(TextReader reader);
    }
}