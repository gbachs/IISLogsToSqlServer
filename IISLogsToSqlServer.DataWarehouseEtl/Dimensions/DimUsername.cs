using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.DataWarehouseEtl.Dimensions
{
    [Table("DimUsername")]
    public class DimUsername
    {
        public DimUsername(string username)
        {
            Username = username;
        }

        public DimUsername()
        {
        }

        [Key]
        public int UsernameKey { get; set; }
        public string Username { get; set; }
    }
}