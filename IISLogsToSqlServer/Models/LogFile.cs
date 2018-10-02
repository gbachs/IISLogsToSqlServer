using System;
using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.Models
{
    [Table("LogFiles")]
    public class LogFile
    {
        [ExplicitKey]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ServerId { get; set; }
    }
}
