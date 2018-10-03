using System;
using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.Parser.Models
{
    [Table("RawLogs")]
    public class LogEvent
    {
        [Key] public long Id { get; set; }


        public long FileId { get; set; }

        [FieldDate("date")] [Write(false)] public DateTimeOffset Date { get; set; }

        [FieldTime("time")] [Write(false)] public DateTimeOffset Time { get; set; }

        private DateTime? _dateTime;

        public DateTime DateTime
        {
            get
            {
                if (_dateTime != null)
                    return (DateTime) _dateTime;

                return new DateTime(Date.Year, Date.Month, Date.Day, Time.Hour, Time.Minute, Time.Second,
                    Time.Millisecond);
            }
            set => _dateTime = value;
        }

        [Field("s-ip")] public string ServerIpAddress { get; set; }

        [Field("cs-method")] public string Method { get; set; }

        [Field("cs-uri-stem")] public string UriStem { get; set; }

        [Field("cs-uri-query")] public string UriQuery { get; set; }

        [Int32("s-port")] public int Port { get; set; }

        [Field("cs-username")] public string Username { get; set; }

        [Field("c-ip")] public string ClientIpAddress { get; set; }

        [Field("cs(User-Agent)")] public string Agent { get; set; }

        [Field("cs(Referer)")] public string Referer { get; set; }

        [Int32("sc-status")] public int Status { get; set; }

        [Int32("sc-substatus")] public int SubStatus { get; set; }

        [Int64("sc-win32-status")] public long Win32Status { get; set; }

        [Int32("time-taken")] public int TimeTaken { get; set; }

        [Int32("sc-bytes")] public int BytesSent { get; set; }

        [Int32("cs-bytes")] public int BytesReceived { get; set; }

        [Field("host")] public string Host { get; set; }

        public bool Processed { get; set; }
    }
}