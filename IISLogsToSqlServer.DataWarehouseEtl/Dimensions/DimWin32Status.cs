using Dapper.Contrib.Extensions;

namespace IISLogsToSqlServer.DataWarehouseEtl.Dimensions
{
    [Table("DimWin32Status")]
    public class DimWin32Status
    {
        public DimWin32Status(int win32Status)
        {
            Win32Status = win32Status;
        }
        public DimWin32Status()
        {
        }

        [Key]
        public int Win32StatusKey { get; set; }
        public int Win32Status { get; set; }
    }
}