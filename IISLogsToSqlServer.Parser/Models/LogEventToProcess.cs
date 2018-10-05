using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.Parser.Models
{
    [Table("RawLogsToProcess")]
    public class LogEventToProcess : LogEvent
    {
        public LogEventToProcess()
        {

        }

        public LogEventToProcess(LogEvent x)
        {
            Date = x.Date;
            Time = x.Time;
            Agent = x.Agent;
            BytesReceived = x.BytesReceived;
            BytesSent = x.BytesSent;
            ClientIpAddress = x.ClientIpAddress;
            FileId = x.FileId;
            Host = x.Host;
            Method = x.Method;
            Port = x.Port;
            Referer = x.Referer;
            UriQuery = x.UriQuery;
            Win32Status = x.Win32Status;
            Status = x.Status;
            SubStatus = x.SubStatus;
            UriStem = x.UriStem;
            TimeTaken = x.TimeTaken;
            ServerIpAddress = x.ServerIpAddress;
            Username = x.Username;
        }
    }
}