using System.IO;
using System.Linq;
using IISLogsToSqlServer.Common;
using IISLogsToSqlServer.Common.Models;
using IISLogsToSqlServer.Common.Repositories.Interfaces;
using IISLogsToSqlServer.Parser.Interfaces;
using IISLogsToSqlServer.Parser.Models;
using IISLogsToSqlServer.Services.Interfaces;

namespace IISLogsToSqlServer.Services
{
    public class ParseLogsForServerService : IParseLogsForServerService
    {
        private readonly IRepository<LogFile> _logFileRepository;
        private readonly IRepository<LogEvent> _rawLogsRepository;
        private readonly IIisLogReader _iisLogReader;
        private readonly ILogger _logger;

        public ParseLogsForServerService(IRepository<LogFile> logFileRepository,
            IIisLogReader iisLogReader,
            ILogger logger,
            IRepository<LogEvent> rawLogsRepository)
        {
            _logFileRepository = logFileRepository;
            _iisLogReader = iisLogReader;
            _logger = logger;
            _rawLogsRepository = rawLogsRepository;
        }

        public void Execute(Server server, string severLogsFolder)
        {
            var existingFiles = _logFileRepository.GetAll().ToDictionary(x => x.Name);

            foreach (var file in Directory.GetFiles(severLogsFolder))
            {
                var logFileInfo = new FileInfo(file);

                if (existingFiles.TryGetValue(logFileInfo.Name, out var logFile))
                {
                    MoveLogFileToCompletedFolder(severLogsFolder, logFileInfo);
                    continue;
                }

                logFile = new LogFile
                {
                    Name = logFileInfo.Name,
                    ServerId = server.Id
                };

                _logFileRepository.Add(logFile);

                ParseLogFile(server, logFile, file);

                MoveLogFileToCompletedFolder(severLogsFolder, logFileInfo);
            }
        }

        private static void MoveLogFileToCompletedFolder(string severLogsFolder, FileInfo logFileInfo)
        {
            var completedFolder = new DirectoryInfo(Path.Combine(severLogsFolder, "Completed"));

            if (!completedFolder.Exists)
            {
                File.Move(logFileInfo.FullName, Path.Combine(completedFolder.FullName, logFileInfo.Name));
            }
        }


        private void ParseLogFile(Server server, LogFile logFile, string filePath)
        {
            _logger.Log($"Parsing log file: {filePath} for server: {server.Name}");

            var data = _iisLogReader.Read(File.OpenText(filePath)).ToList();

            data.ForEach(x =>
            {
                x.FileId = logFile.Id;
            });

            _rawLogsRepository.BulkAdd(data);

            _logger.Log($"Parsed log file: {filePath} for server: {server.Name}");
        }
    }
}
