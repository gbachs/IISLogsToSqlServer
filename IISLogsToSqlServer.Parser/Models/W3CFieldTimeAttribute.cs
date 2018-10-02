using IISLogsToSqlServer.Parser.Convertors;

namespace IISLogsToSqlServer.Parser.Models
{
    internal class W3CFieldTimeAttribute : W3CFieldBaseAttribute
    {
        public W3CFieldTimeAttribute(string name) : base(name, new TimeConvertor())
        {
        }
    }
}