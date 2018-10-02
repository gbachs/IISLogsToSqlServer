namespace IISLogsToSqlServer.Parser.Convertors
{
    public interface ITextConvertor
	{
		dynamic Convert(string text);
	}
}
