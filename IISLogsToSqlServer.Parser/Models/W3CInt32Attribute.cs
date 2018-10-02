using IISLogsToSqlServer.Parser.Convertors;

namespace IISLogsToSqlServer.Parser.Models
{
    internal class W3CInt32Attribute : W3CFieldBaseAttribute
    {
        public W3CInt32Attribute(string name) : base(name, new Int32Convertor())
        {
        }
    }
}