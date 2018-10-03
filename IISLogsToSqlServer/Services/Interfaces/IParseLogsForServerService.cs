using IISLogsToSqlServer.Common.Models;

namespace IISLogsToSqlServer.Services.Interfaces
{
    public interface IParseLogsForServerService
    {
        void Execute(Server server, string folderPath);
    }
}