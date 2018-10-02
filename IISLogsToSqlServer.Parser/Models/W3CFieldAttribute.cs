using IISLogsToSqlServer.Parser.Convertors;

namespace IISLogsToSqlServer.Parser.Models
{
    internal class W3CFieldAttribute : W3CFieldBaseAttribute
    {
        public W3CFieldAttribute(string name) : base(name, new StringConvertor())
        {
        }
    }
}