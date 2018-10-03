using System.Collections.Generic;
using FastMember;
using IISLogsToSqlServer.Parser.Convertors;

namespace IISLogsToSqlServer.Parser.Models
{
    public class FieldMapping : Dictionary<int, FieldMapInfo>
    {
        public FieldMapping() : base(16)
        {

        }
    }

    public sealed class FieldMapInfo
    {
        public readonly ITextConvertor Convertor;
        private readonly TypeAccessor _typeAccessor;
        private readonly string _propertyName;

        public FieldMapInfo(ITextConvertor convertorType, TypeAccessor typeAccessor, string propertyName)
        {
            Convertor = convertorType;
            _typeAccessor = typeAccessor;
            _propertyName = propertyName;
        }

        public void SetValue(LogEvent logEventObject, object value)
        {
            _typeAccessor[logEventObject, _propertyName] = value;
        }
    }
}