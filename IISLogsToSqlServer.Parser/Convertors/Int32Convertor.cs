﻿namespace IISLogsToSqlServer.Parser.Convertors
{
    public class Int32Convertor : ITextConvertor
    {
        public dynamic Convert(string text) => System.Convert.ToInt32(text);
    }
}
