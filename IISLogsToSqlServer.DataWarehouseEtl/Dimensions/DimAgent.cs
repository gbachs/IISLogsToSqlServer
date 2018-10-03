using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.DataWarehouseEtl.Dimensions
{
    [Table("DimAgent")]
    public class DimAgent
    {
        public DimAgent(string agent)
        {
            Agent = agent;
        }
        public DimAgent()
        {
        }

        [Key]
        public int AgentKey { get; set; }
        public string Agent { get; set; }
    }
}