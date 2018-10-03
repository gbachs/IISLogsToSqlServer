using System.Collections.Generic;
using System.IO;
using IISLogsToSqlServer.Parser.Models;

namespace IISLogsToSqlServer.Parser.Interfaces
{
    public interface IIisLogReader
    {
        IEnumerable<LogEvent> Read(TextReader reader);
    }
}