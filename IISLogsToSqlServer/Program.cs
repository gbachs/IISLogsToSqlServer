using System;
using System.IO;
using System.Linq;
using IISLogsToSqlServer.Common;
using IISLogsToSqlServer.Common.Models;
using IISLogsToSqlServer.Common.Repositories.Interfaces;
using IISLogsToSqlServer.DataWarehouseEtl.Services;
using IISLogsToSqlServer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IISLogsToSqlServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = IoC.Initialize();
            var logger = container.GetService<ILogger>();

            UpdateRawLogs(logger, container);
            UpdateWarehouse(logger, container);
        }

        private static void UpdateWarehouse(ILogger logger, IServiceProvider container)
        {
            logger.Log("Update Warehouse");

            var updateWarehouseService = container.GetService<IUpdateDataWarehouseService>();
            updateWarehouseService.Update();
        }

        private static void UpdateRawLogs(ILogger logger, IServiceProvider container)
        {
            logger.Log("Ingesting logs");

            var serverRepository = container.GetService<IRepository<Server>>();
            var parseLogsForServerService = container.GetService<IParseLogsForServerService>();

            var servers = serverRepository.GetAll().ToDictionary(x => x.Name);

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
        }
    }
}
