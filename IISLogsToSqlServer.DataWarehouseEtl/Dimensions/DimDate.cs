using System;
using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.DataWarehouseEtl.Dimensions
{
    [Table("DimDate")]
    public class DimDate
    {
        [Key]
        public long DateKey { get; set; }
        public DateTime Date { get; set; }
    }
}
