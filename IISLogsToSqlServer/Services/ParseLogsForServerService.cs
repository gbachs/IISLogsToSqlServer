using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ILogFileRepository _logFileRepository;
        private readonly IRepository<LogEventToProcess> _toProcessRepository;
        private readonly IIISEventLogReader _iisLogReader;
        private readonly ILogger _logger;

        public ParseLogsForServerService(ILogFileRepository logFileRepository,
            IIISEventLogReader iisLogReader,
            ILogger logger,
            IRepository<LogEventToProcess> toProcessRepository)
        {
            _logFileRepository = logFileRepository;
            _iisLogReader = iisLogReader;
            _logger = logger;
            _toProcessRepository = toProcessRepository;
        }

        public void Execute(Server server, string severLogsFolder)
        {
            var existingFiles = _logFileRepository.GetAllForServer(server.Id).ToDictionary(x => x.Name);

            var filesOnDisk = Directory.GetFiles(severLogsFolder);

            Parallel.ForEach(filesOnDisk, GetParallelOptions(), file =>
            {
                var logFileInfo = new FileInfo(file);

                if (!existingFiles.TryGetValue(logFileInfo.Name, out var logFile))
                {
                    logFile = new LogFile
                    {
                        Name = logFileInfo.Name,
                        ServerId = server.Id
                    };

                    _logFileRepository.Add(logFile);

                    ParseLogFile(server, logFile, file);
                }

                MoveLogFileToCompletedFolder(severLogsFolder, logFileInfo);
            });
        }

        private static ParallelOptions GetParallelOptions()
        {
            return new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };
        }

        private static void MoveLogFileToCompletedFolder(string severLogsFolder, FileSystemInfo logFileInfo)
        {
            var completedFolder = new DirectoryInfo(Path.Combine(severLogsFolder, "Completed"));

            if (!completedFolder.Exists)
            {
                Directory.CreateDirectory(completedFolder.FullName);
            }

            File.Move(logFileInfo.FullName, Path.Combine(completedFolder.FullName, logFileInfo.Name));
        }

        private void ParseLogFile(Server server, LogFile logFile, string filePath)
        {
            _logger.Log($"Parsing log file: {filePath} for server: {server.Name}");

            var data = ReadLogFile(filePath);

            data.ForEach(x =>
            {
                x.FileId = logFile.Id;
            });

            _toProcessRepository.BulkAdd(data.Select(x => new LogEventToProcess(x)).ToList());

            _logger.Log($"Parsed log file: {filePath} for server: {server.Name}");
        }


        private List<LogEvent> ReadLogFile(string filePath)
        {
            using (var fileReader = File.OpenText(filePath))
            {
                return _iisLogReader.Read(fileReader).ToList();
            }
        }
    }
}
