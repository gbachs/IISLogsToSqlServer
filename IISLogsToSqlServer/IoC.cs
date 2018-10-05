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
    public static class IoC
    {
        public static ServiceProvider Initialize()
        {
            var serviceProvider = new ServiceCollection()
                  //common
                  .AddSingleton<IConnectionInfo>(new ConnectionInfo())
                  .AddSingleton<ILogger, Logger>()

                  //repositories
                  .AddSingleton<ILogEventToProcessRepository, LogEventToProcessRepository>()
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
                  .AddSingleton<IRepository<LogEventToProcess>, BaseRepository<LogEventToProcess>>()

                  //services
                  .AddSingleton<IUpdateDataWarehouseService, UpdateDataWarehouseService>()
                  .AddSingleton<IParseLogsForServerService, ParseLogsForServerService>()
                  .AddSingleton<IIISEventLogReader, IISEventLogReader>()

                  .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
