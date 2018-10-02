using System;
using System.Globalization;

namespace IISLogsToSqlServer.Parser.Convertors
{
    public class TimeConvertor : ITextConvertor
    {
        public dynamic Convert(string text) => DateTimeOffset.ParseExact(text, "HH':'mm':'ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
    }
}
