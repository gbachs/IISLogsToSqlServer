using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.DataWarehouseEtl.Dimensions
{
    [Table("DimPort")]
    public class DimPort
    {
        public DimPort(int port)
        {
            Port = port;
        }

        public DimPort()
        {
        }

        [Key]
        public int PortKey { get; set; }
        public int Port { get; set; }
    }
}