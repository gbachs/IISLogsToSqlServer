using IISLogsToSqlServer.Parser.Convertors;

namespace IISLogsToSqlServer.Parser.Models
{
    internal class FieldTimeAttribute : FieldBaseAttribute
    {
        public FieldTimeAttribute(string name) : base(name, new TimeConvertor())
        {
        }
    }
}