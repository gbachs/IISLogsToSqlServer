using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.DataWarehouseEtl.Dimensions
{
    [Table("DimServerIp")]
    public class DimServerIp
    {
        public DimServerIp(string serverIpAddress)
        {
            ServerIpAddress = serverIpAddress;
        }
        public DimServerIp()
        {
        }

        [Key]
        public int ServerIpKey { get; set; }
        public string ServerIpAddress { get; set; }
    }
}