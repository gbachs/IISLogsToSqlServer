using System;
using IISLogsToSqlServer.Parser.Convertors;

namespace IISLogsToSqlServer.Parser.Models
{
    public abstract class FieldBaseAttribute : Attribute
    {
        public readonly ITextConvertor Convertor;
        public readonly string FieldName;

        protected FieldBaseAttribute(string name)
        {
            FieldName = name;
        }

        protected FieldBaseAttribute(string name, ITextConvertor convertor)
        {
            FieldName = name;
            Convertor = convertor;
        }
    }
}
