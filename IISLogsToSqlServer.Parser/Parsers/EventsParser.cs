using IISLogsToSqlServer.Parser.Models;

namespace IISLogsToSqlServer.Parser.Parsers
{
    internal sealed class EventsParser
    {
        public LogEvent Parse(string line, FieldMapping fieldMap)
        {
            var returnValue = new LogEvent();
            var fieldValueIndex = 0;

            foreach (var fieldValue in line.Split(' '))
            {
                if (fieldMap.TryGetValue(fieldValueIndex, out var fieldInfo))
                {
                    fieldInfo.SetValue(returnValue, fieldInfo.Convertor.Convert(fieldValue));
                }

                fieldValueIndex += 1;
            }

            return returnValue;
        }
    }
}