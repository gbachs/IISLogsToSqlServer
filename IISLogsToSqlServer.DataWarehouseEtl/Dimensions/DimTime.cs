using System;
using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.DataWarehouseEtl.Dimensions
{
    [Table("DimTime")]
    public class DimTime
    {
        [Key]
        public long TimeKey { get; set; }
        public TimeSpan Time { get; set; }
    }
}