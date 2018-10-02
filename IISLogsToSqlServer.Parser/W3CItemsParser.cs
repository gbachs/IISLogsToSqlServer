using IISLogsToSqlServer.Parser.Models;

namespace IISLogsToSqlServer.Parser
{
    internal sealed class W3CItemsParser
    {
        public W3CEvent Parse(string line, W3CFieldMap fieldMap)
        {
            var returnValue = new W3CEvent();
            var fieldValueIndex = 0;

            foreach (var fieldValue in line.Split(' '))
            {
                if (fieldMap.ContainsKey(fieldValueIndex))
                {
                    var fieldInfo = fieldMap[fieldValueIndex];
                    fieldInfo.FieldInfo.SetValue(returnValue, fieldInfo.Convertor.Convert(fieldValue));
                }

                fieldValueIndex += 1;
            }

            return returnValue;
        }
    }
}