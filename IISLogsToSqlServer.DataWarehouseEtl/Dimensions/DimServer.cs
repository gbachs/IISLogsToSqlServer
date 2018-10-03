using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.DataWarehouseEtl.Dimensions
{
    [Table("DimServer")]
    public class DimServer
    {
        public DimServer(string serverName)
        {
            ServerName = serverName;
        }

        public DimServer()
        {
        }

        [Key]
        public int ServerKey { get; set; }
        public string ServerName { get; set; }
    }
}