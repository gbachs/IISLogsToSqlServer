using IISLogsToSqlServer.Parser.Convertors;

namespace IISLogsToSqlServer.Parser.Models
{
    internal class Int64Attribute : FieldBaseAttribute
    {
        public Int64Attribute(string name) : base(name, new Int64Convertor())
        {
        }
    }
}