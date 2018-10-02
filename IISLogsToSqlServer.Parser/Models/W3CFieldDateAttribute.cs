using IISLogsToSqlServer.Parser.Convertors;

namespace IISLogsToSqlServer.Parser.Models
{
    internal class W3CFieldDateAttribute : W3CFieldBaseAttribute
    {
        public W3CFieldDateAttribute(string name) : base(name, new DateTimeOffsetConvertor())
        {
        }
    }
}