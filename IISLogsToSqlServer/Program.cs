using System;
using System.IO;
using System.Linq;
using IISLogsToSqlServer.Models;
using IISLogsToSqlServer.Parser;
using IISLogsToSqlServer.Parser.Interfaces;
using IISLogsToSqlServer.Parser.Models;
using IISLogsToSqlServer.Repositories;
using IISLogsToSqlServer.Services;
using IISLogsToSqlServer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IISLogsToSqlServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = CreateContainer();

            var serverRepository = container.GetService<IRepository<Server>>();
            var parseLogsForServerService = container.GetService<IParseLogsForServerService>();

            var servers = serverRepository.GetAll().ToDictionary(x=> x.Name);

            foreach (var directory in Directory.GetDirectories("c:\\temp\\IISLogs"))
            {
                var directoryInfo = new DirectoryInfo(directory);

                if (!servers.TryGetValue(directoryInfo.Name, out var server))
                {
                    server = new Server
                    {
                        Id = Guid.NewGuid(),
                        Name = directoryInfo.Name
                    };
                    serverRepository.Add(server);
                }

                parseLogsForServerService.Execute(server, directory);
            }

            //var dimensionSyncServices = serviceProvider.GetServices<IDimensionSync>();

            //foreach (var dimensionSync in dimensionSyncServices.OrderBy(x => x.Order))
            //{
            //    dimensionSync.Sync();
            //}
        }

        private static ServiceProvider CreateContainer()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConnectionInfo>(new ConnectionInfo())
                .AddSingleton<IRepository<Server>, BaseRepository<Server>>()
                .AddSingleton<IRepository<LogFile>, BaseRepository<LogFile>>()
                .AddSingleton<IIisLogReader, IisLogReader>()
                .AddSingleton<IRepository<W3CEvent>, BaseRepository<W3CEvent>>()
                .AddSingleton<IParseLogsForServerService, ParseLogsForServerService>()
                .AddSingleton<ILogger, Logger>()
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
