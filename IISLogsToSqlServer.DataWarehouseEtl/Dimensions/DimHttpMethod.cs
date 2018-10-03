using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.DataWarehouseEtl.Dimensions
{
    [Table("DimHttpMethod")]
    public class DimHttpMethod
    {
        public DimHttpMethod(string method)
        {
            HttpMethod = method;
        }

        public DimHttpMethod()
        {
        }

        [Key]
        public int HttpMethodKey { get; set; }
        public string HttpMethod { get; set; }
    }
}