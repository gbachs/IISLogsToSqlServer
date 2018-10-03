namespace IISLogsToSqlServer.Parser.Convertors
{
    public class Int64Convertor : ITextConvertor
    {
        public dynamic Convert(string text) => System.Convert.ToInt64(text);
    }
}