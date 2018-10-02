using System;
using System.Security.Cryptography;
using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.Parser.Models
{
    [Table("RawLogs")]
    public class W3CEvent
    {
        [ExplicitKey]

        public Guid Id { get; set; }


        public Guid FileId { get; set; }

        [W3CFieldDate("date")]
        public DateTimeOffset Date { get; set; }

        [W3CFieldTime("time")]
        public DateTimeOffset Time { get; set; }

        [W3CField("s-ip")]

        public string ServerIpAddress { get; set; }

        [W3CField("cs-method")]

        public string Method { get; set; }

        [W3CField("cs-uri-stem")]

        public string UriStem { get; set; }

        [W3CField("cs-uri-query")]

        public string UriQuery { get; set; }

        [W3CInt32("s-port")]

        public int Port { get; set; }

        [W3CField("cs-username")]

        public string Username { get; set; }

        [W3CField("c-ip")]

        public string ClientIpAddress { get; set; }

        [W3CField("cs(User-Agent)")]

        public string Agent { get; set; }

        [W3CField("cs(Referer)")]

        public string Referer { get; set; }

        [W3CInt32("sc-status")]

        public int Status { get; set; }

        [W3CInt32("sc-substatus")]

        public int SubStatus { get; set; }

        [W3CInt32("sc-win32-status")]

        public int Win32Status { get; set; }

        [W3CInt32("time-taken")]

        public int TimeTaken { get; set; }

        [W3CInt32("sc-bytes")]

        public int BytesSent { get; set; }

        [W3CInt32("cs-bytes")]

        public int BytesReceived { get; set; }

        [W3CField("host")]

        public string Host { get; set; }
    }
}
