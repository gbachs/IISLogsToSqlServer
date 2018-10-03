using System;
using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.Common.Models
{
    [Table("Servers")]
    public class Server
    {
        [ExplicitKey]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
