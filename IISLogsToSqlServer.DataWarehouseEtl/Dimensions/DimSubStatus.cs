using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.DataWarehouseEtl.Dimensions
{
    [Table("DimSubStatus")]
    public class DimSubStatus
    {
        public DimSubStatus(int subStatus)
        {
            SubStatus = subStatus;
        }
        public DimSubStatus()
        {

        }

        [Key]
        public int SubStatusKey { get; set; }
        public int SubStatus { get; set; }
    }
}