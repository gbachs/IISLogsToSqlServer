using IISLogsToSqlServer.Parser.Convertors;

namespace IISLogsToSqlServer.Parser.Models
{
    internal class Int32Attribute : FieldBaseAttribute
    {
        public Int32Attribute(string name) : base(name, new Int32Convertor())
        {
        }
    }
}