using IISLogsToSqlServer.Parser.Convertors;

namespace IISLogsToSqlServer.Parser.Models
{
    internal class FieldAttribute : FieldBaseAttribute
    {
        public FieldAttribute(string name) : base(name, new StringConvertor())
        {
        }
    }
}