using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.DataWarehouseEtl.Dimensions
{
    [Table("DimStatus")]
    public class DimStatus
    {
        public DimStatus(int status)
        {
            Status = status;
        }

        public DimStatus()
        {
        }

        [Key]
        public int StatusKey { get; set; }
        public int Status { get; set; }
    }
}