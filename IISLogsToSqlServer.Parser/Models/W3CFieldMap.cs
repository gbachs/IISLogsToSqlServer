using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IISLogsToSqlServer.Parser.Convertors;

namespace IISLogsToSqlServer.Parser.Models
{
    public sealed class W3CFieldMap
    {
        private readonly Dictionary<int, W3CFieldMapInfo> _fieldDictionary = new Dictionary<int, W3CFieldMapInfo>(16);

        public sealed class W3CFieldMapInfo
        {
            public readonly ITextConvertor Convertor;
            public readonly PropertyInfo FieldInfo;

            public W3CFieldMapInfo(ITextConvertor convertorType, PropertyInfo fieldInfo)
            {
                Convertor = convertorType;
                FieldInfo = fieldInfo;
            }
        }

        public bool IsEmpty()
        {
            return !_fieldDictionary.Any();
        }

        public bool ContainsKey(int key)
        {
            return _fieldDictionary.ContainsKey(key);
        }

        public W3CFieldMapInfo this[int key] => _fieldDictionary[key];

        public void Add(int fieldIndex, ITextConvertor convertorType, PropertyInfo fieldInfo)
        {
            _fieldDictionary.Add(fieldIndex, new W3CFieldMapInfo(convertorType, fieldInfo));
        }
    }
}