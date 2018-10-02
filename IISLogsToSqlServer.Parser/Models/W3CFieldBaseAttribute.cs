using System;
using IISLogsToSqlServer.Parser.Convertors;

namespace IISLogsToSqlServer.Parser.Models
{
    public abstract class W3CFieldBaseAttribute : Attribute
    {
        public readonly ITextConvertor Convertor;
        public readonly string FieldName;

        protected W3CFieldBaseAttribute(string name)
        {
            FieldName = name;
        }

        protected W3CFieldBaseAttribute(string name, ITextConvertor convertor)
        {
            FieldName = name;
            Convertor = convertor;
        }
    }
}
