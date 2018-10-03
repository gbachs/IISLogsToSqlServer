using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.DataWarehouseEtl.Facts
{
    [Table("FactEvent")]
    public class FactEvent
    {
        [Key]
        public long EventId { get; set; }
        public long ReferenceId { get; set; }
        public long DateKey { get; set; }
        public long TimeKey { get; set; }
        public int AgentKey { get; set; }
        public int ClientIpKey { get; set; }
        public int HttpMethodKey { get; set; }
        public int PortKey { get; set; }
        public int ServerKey { get; set; }
        public int ServerIpKey { get; set; }
        public int StatusKey { get; set; }
        public int SubStatusKey { get; set; }
        public int Win32StatusKey { get; set; }
        public int UserNameKey { get; set; }
        public string UriStem { get; set; }
        public string UriQuery { get; set; }
        public int TimeTaken { get; set; }
        public int BytesSent { get; set; }
        public int BytesReceived { get; set; }
    }
}
