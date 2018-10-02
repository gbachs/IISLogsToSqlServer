using System;
using System.Collections.Generic;
using System.IO;
using IISLogsToSqlServer.Parser.Interfaces;
using IISLogsToSqlServer.Parser.Models;

namespace IISLogsToSqlServer.Parser
{
    public class IisLogReader : IIisLogReader
    {
        public IEnumerable<W3CEvent> Read(TextReader reader)
        {
            var itemParser = new W3CItemsParser();
            var fieldMap = (W3CFieldMap)null;
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("#", StringComparison.OrdinalIgnoreCase))
                {
                    ParseHeader(line, fieldsLine => { fieldMap = new W3CFieldsParser().Parse(fieldsLine); });
                    continue;
                }

                if (fieldMap == null)
                    continue;

                yield return itemParser.Parse(line, fieldMap);
            }
        }

        private static void ParseHeader(string line, Action<string> fieldsBlock)
        {
            const string commentFields = "#Fields";
            const string commentSoftware = "#Software";

            if (line.StartsWith(commentSoftware, StringComparison.OrdinalIgnoreCase))
            {
                //    Software = RightOf(commentSoftware, line);
            }
            else if (line.StartsWith(commentFields, StringComparison.OrdinalIgnoreCase))
            {
                fieldsBlock(RightOf(commentFields, line));
            }
        }
        private static string RightOf(string token, string line)
        {
            return line.Substring(token.Length + 2);
        }
    }
}