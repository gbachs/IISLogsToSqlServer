namespace IISLogsToSqlServer.Parser.Convertors
{
    public class StringConvertor : ITextConvertor
    {
        public dynamic Convert(string text) => text;
    }
}
