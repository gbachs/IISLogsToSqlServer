using System;
using System.IO;
using System.Linq;
using IISLogsToSqlServer.Models;
using IISLogsToSqlServer.Parser.Interfaces;
using IISLogsToSqlServer.Parser.Models;
using IISLogsToSqlServer.Repositories;
using IISLogsToSqlServer.Services.Interfaces;

namespace IISLogsToSqlServer.Services
{
    public class ParseLogsForServerService : IParseLogsForServerService
    {
        private readonly IRepository<LogFile> _logFileRepository;
        private readonly IRepository<W3CEvent> _rawLogsRepository;
        private readonly IIisLogReader _iisLogReader;
        private readonly ILogger _logger;

        public ParseLogsForServerService(IRepository<LogFile> logFileRepository,
            IIisLogReader iisLogReader,
            ILogger logger,
            IRepository<W3CEvent> rawLogsRepository)
        {
            _logFileRepository = logFileRepository;
            _iisLogReader = iisLogReader;
            _logger = logger;
            _rawLogsRepository = rawLogsRepository;
        }

        public void Execute(Server server, string folderPath)
        {
            var existingFiles = _logFileRepository.GetAll().ToDictionary(x => x.Name);

            foreach (var file in Directory.GetFiles(folderPath))
            {
                var fileInfo = new FileInfo(file);

                if (existingFiles.TryGetValue(fileInfo.Name, out var logFile))
                    continue;

                logFile = new LogFile
                {
                    Id = Guid.NewGuid(),
                    Name = fileInfo.Name,
                    ServerId = server.Id
                };

                _logFileRepository.Add(logFile);

                ParseLogFile(server, logFile, file);
            }
        }

        private void ParseLogFile(Server server, LogFile logFile, string filePath)
        {
            _logger.Log($"Parsing log file: {filePath} for server: {server.Name}");

            var data = _iisLogReader.Read(File.OpenText(filePath)).ToList();

            data.ForEach(x =>
            {
                x.FileId = logFile.Id;
                x.Id = Guid.NewGuid();

            });

            _rawLogsRepository.BulkAdd(data);

            _logger.Log($"Parsed log file: {filePath} for server: {server.Name}");
        }
    }
}
