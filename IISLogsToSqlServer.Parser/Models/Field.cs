using System.Reflection;
using IISLogsToSqlServer.Parser.Convertors;

namespace IISLogsToSqlServer.Parser.Models
{
    public sealed class Field
    {
        public readonly ITextConvertor Convertor;
        public readonly PropertyInfo FieldInfo;

        public Field(ITextConvertor convertorType, PropertyInfo propertyInfo)
        {
            Convertor = convertorType;
            FieldInfo = propertyInfo;
        }
    }
}