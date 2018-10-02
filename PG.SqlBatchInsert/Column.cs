using System.Data;

namespace PG.SqlBatchInsert
{
    public class Column
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public SqlDbType DataType { get; set; }
        public int Length { get; set; }
    }
}