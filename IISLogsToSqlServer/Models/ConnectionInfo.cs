using System.IO;
using Microsoft.Extensions.Configuration;

namespace IISLogsToSqlServer.Models
{
    public class ConnectionInfo : IConnectionInfo
    {
        public ConnectionInfo()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();
            ConnectionString = configuration.GetConnectionString("IISLogs");
        }

        public string ConnectionString { get; set; }
    }
}