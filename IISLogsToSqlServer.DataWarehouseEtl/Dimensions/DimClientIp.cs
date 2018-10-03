using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.DataWarehouseEtl.Dimensions
{
    [Table("DimClientIp")]
    public class DimClientIp
    {
        public DimClientIp(string clientIpAddress)
        {
            ClientIpAddress = clientIpAddress;
        }

        public DimClientIp()
        {
        }

        [Key] public int ClientIpKey { get; set; }

        public string ClientIpAddress { get; set; }
    }
}