using System;
using System.IO;
using System.Linq;
using IISLogsToSqlServer.Common;
using IISLogsToSqlServer.Common.Models;
using IISLogsToSqlServer.Common.Repositories;
using IISLogsToSqlServer.Common.Repositories.Interfaces;
using IISLogsToSqlServer.DataWarehouseEtl.Dimensions;
using IISLogsToSqlServer.DataWarehouseEtl.Facts;
using IISLogsToSqlServer.DataWarehouseEtl.Services;
using IISLogsToSqlServer.Parser;
using IISLogsToSqlServer.Parser.Interfaces;
using IISLogsToSqlServer.Parser.Models;
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
            logger.Log("Injesting logs");

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

        private static ServiceProvider CreateContainer()
        {
            var serviceProvider = new ServiceCollection()
                //common
                .AddSingleton<IConnectionInfo>(new ConnectionInfo())
                .AddSingleton<ILogger, Logger>()

                //repositories
                .AddSingleton<ILogEventRepository, LogEventRepository>()
                .AddSingleton<IRepository<Server>, BaseRepository<Server>>()
                .AddSingleton<IRepository<LogFile>, BaseRepository<LogFile>>()
                .AddSingleton<IRepository<LogEvent>, BaseRepository<LogEvent>>()
                .AddSingleton<IRepository<DimAgent>, BaseRepository<DimAgent>>()
                .AddSingleton<IRepository<DimClientIp>, BaseRepository<DimClientIp>>()
                .AddSingleton<IRepository<DimDate>, BaseRepository<DimDate>>()
                .AddSingleton<IRepository<DimHttpMethod>, BaseRepository<DimHttpMethod>>()
                .AddSingleton<IRepository<DimPort>, BaseRepository<DimPort>>()
                .AddSingleton<IRepository<DimServer>, BaseRepository<DimServer>>()
                .AddSingleton<IRepository<DimServerIp>, BaseRepository<DimServerIp>>()
                .AddSingleton<IRepository<DimStatus>, BaseRepository<DimStatus>>()
                .AddSingleton<IRepository<DimSubStatus>, BaseRepository<DimSubStatus>>()
                .AddSingleton<IRepository<DimTime>, BaseRepository<DimTime>>()
                .AddSingleton<IRepository<DimUsername>, BaseRepository<DimUsername>>()
                .AddSingleton<IRepository<DimWin32Status>, BaseRepository<DimWin32Status>>()
                .AddSingleton<IRepository<FactEvent>, BaseRepository<FactEvent>>()

                //services
                .AddSingleton<IUpdateDataWarehouseService, UpdateDataWarehouseService>()
                .AddSingleton<IParseLogsForServerService, ParseLogsForServerService>()
                .AddSingleton<IIisLogReader, IisLogReader>()

                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
