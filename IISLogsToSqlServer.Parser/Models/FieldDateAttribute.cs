using IISLogsToSqlServer.Parser.Convertors;

namespace IISLogsToSqlServer.Parser.Models
{
    internal class FieldDateAttribute : FieldBaseAttribute
    {
        public FieldDateAttribute(string name) : base(name, new DateTimeOffsetConvertor())
        {
        }
    }
}