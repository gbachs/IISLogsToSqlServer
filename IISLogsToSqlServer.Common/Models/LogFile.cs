using System;
using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.Common.Models
{
    [Table("LogFiles")]
    public class LogFile
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public Guid ServerId { get; set; }
    }
}
