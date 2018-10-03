using System.Collections.Generic;
using System.Linq;
using IISLogsToSqlServer.Common;
using IISLogsToSqlServer.Common.Models;
using IISLogsToSqlServer.Common.Repositories.Interfaces;
using IISLogsToSqlServer.DataWarehouseEtl.Dimensions;
using IISLogsToSqlServer.DataWarehouseEtl.Facts;
using IISLogsToSqlServer.Parser.Models;

namespace IISLogsToSqlServer.DataWarehouseEtl.Services
{
    public class UpdateDataWarehouseService : IUpdateDataWarehouseService
    {
        private readonly ILogEventRepository _logEventRepository;
        private readonly IRepository<DimAgent> _dimAgentRepository;
        private readonly IRepository<DimClientIp> _dimClientIpRepository;
        private readonly IRepository<DimDate> _dimDateRepository;
        private readonly IRepository<DimHttpMethod> _dimHttpMethodRepository;
        private readonly IRepository<DimPort> _dimPortRepository;
        private readonly IRepository<DimServer> _dimServerRepository;
        private readonly IRepository<DimServerIp> _dimServerIpRepository;
        private readonly IRepository<DimStatus> _dimStatusRepository;
        private readonly IRepository<DimSubStatus> _dimSubStatusRepository;
        private readonly IRepository<DimTime> _dimTimeRepository;
        private readonly IRepository<DimUsername> _dimUsernameRepository;
        private readonly IRepository<DimWin32Status> _dimWin32StatusRepository;
        private readonly IRepository<Server> _serverRepository;
        private readonly IRepository<LogFile> _logFileRepository;
        private readonly IRepository<FactEvent> _factEventRepository;
        private readonly ILogger _logger;

        public UpdateDataWarehouseService(ILogEventRepository logEventRepository,
            IRepository<DimClientIp> dimClientIpRepository,
            IRepository<DimDate> dimDateRepository,
            IRepository<DimHttpMethod> dimHttpMethodRepository,
            IRepository<DimServer> dimServerRepository,
            IRepository<DimPort> dimPortRepository,
            IRepository<DimServerIp> dimServerIpRepository,
            IRepository<DimStatus> dimStatusRepository,
            IRepository<DimSubStatus> dimSubStatusRepository,
            IRepository<DimTime> dimTimeRepository,
            IRepository<DimUsername> dimUsernameRepository,
            IRepository<DimWin32Status> dimWin32StatusRepository,
            IRepository<DimAgent> dimAgentRepository,
            IRepository<Server> serverRepository,
            IRepository<LogFile> logFileRepository,
            IRepository<FactEvent> factEventRepository,
            ILogger logger)
        {
            _logEventRepository = logEventRepository;
            _dimClientIpRepository = dimClientIpRepository;
            _dimDateRepository = dimDateRepository;
            _dimHttpMethodRepository = dimHttpMethodRepository;
            _dimServerRepository = dimServerRepository;
            _dimPortRepository = dimPortRepository;
            _dimServerIpRepository = dimServerIpRepository;
            _dimStatusRepository = dimStatusRepository;
            _dimSubStatusRepository = dimSubStatusRepository;
            _dimTimeRepository = dimTimeRepository;
            _dimUsernameRepository = dimUsernameRepository;
            _dimWin32StatusRepository = dimWin32StatusRepository;
            _dimAgentRepository = dimAgentRepository;
            _serverRepository = serverRepository;
            _logFileRepository = logFileRepository;
            _factEventRepository = factEventRepository;
            _logger = logger;
        }

        public void Update()
        {
            _logger.Log("Loading current dimensions");
            var dimClientIps = _dimClientIpRepository.GetAll().ToDictionary(x => x.ClientIpAddress);
            var dimDates = _dimDateRepository.GetAll().ToDictionary(x => x.Date);
            var dimTimes = _dimTimeRepository.GetAll().ToDictionary(x => x.Time);
            var dimHttpMethods = _dimHttpMethodRepository.GetAll().ToDictionary(x => x.HttpMethod);
            var dimServers = _dimServerRepository.GetAll().ToDictionary(x => x.ServerName);
            var dimPorts = _dimPortRepository.GetAll().ToDictionary(x => x.Port);
            var dimServerIps = _dimServerIpRepository.GetAll().ToDictionary(x => x.ServerIpAddress);
            var dimStatuses = _dimStatusRepository.GetAll().ToDictionary(x => x.Status);
            var dimAgents = _dimAgentRepository.GetAll().ToDictionary(x => x.Agent);
            var dimWin32Statuses = _dimWin32StatusRepository.GetAll().ToDictionary(x => x.Win32Status);
            var dimSubStatuses = _dimSubStatusRepository.GetAll().ToDictionary(x => x.SubStatus);
            var dimUsernames = _dimUsernameRepository.GetAll().ToDictionary(x => x.Username);

            var files = _logFileRepository.GetAll().ToDictionary(x => x.Id);
            var servers = _serverRepository.GetAll().ToDictionary(x => x.Id);

            var toInsert = new List<FactEvent>();

            while (GetWork(out var work))
            {
                _logger.Log($"Processing {work.Count} events");

                foreach (var logEvent in work)
                {
                    if (!dimUsernames.TryGetValue(logEvent.Username, out var dimUsername))
                    {
                        dimUsername = new DimUsername(logEvent.Username);
                        dimUsernames.Add(dimUsername.Username, dimUsername);
                        _dimUsernameRepository.Add(dimUsername);
                    }

                    if (!dimSubStatuses.TryGetValue(logEvent.SubStatus, out var dimSubStatus))
                    {
                        dimSubStatus = new DimSubStatus(logEvent.SubStatus);
                        dimSubStatuses.Add(dimSubStatus.SubStatus, dimSubStatus);
                        _dimSubStatusRepository.Add(dimSubStatus);
                    }

                    if (!dimWin32Statuses.TryGetValue((int)logEvent.Win32Status, out var dimWin32Status))
                    {
                        dimWin32Status = new DimWin32Status((int)logEvent.Win32Status);
                        dimWin32Statuses.Add(dimWin32Status.Win32Status, dimWin32Status);
                        _dimWin32StatusRepository.Add(dimWin32Status);
                    }

                    if (!dimAgents.TryGetValue(logEvent.Agent, out var dimAgent))
                    {
                        dimAgent = new DimAgent(logEvent.Agent);
                        dimAgents.Add(dimAgent.Agent, dimAgent);
                        _dimAgentRepository.Add(dimAgent);
                    }

                    if (!dimStatuses.TryGetValue(logEvent.Status, out var dimStatus))
                    {
                        dimStatus = new DimStatus(logEvent.Status);
                        dimStatuses.Add(dimStatus.Status, dimStatus);
                        _dimStatusRepository.Add(dimStatus);
                    }

                    if (!dimServerIps.TryGetValue(logEvent.ServerIpAddress, out var dimServerIp))
                    {
                        dimServerIp = new DimServerIp(logEvent.ServerIpAddress);
                        dimServerIps.Add(dimServerIp.ServerIpAddress, dimServerIp);
                        _dimServerIpRepository.Add(dimServerIp);
                    }

                    if (!dimPorts.TryGetValue(logEvent.Port, out var dimPort))
                    {
                        dimPort = new DimPort(logEvent.Port);
                        dimPorts.Add(dimPort.Port, dimPort);
                        _dimPortRepository.Add(dimPort);
                    }

                    var server = servers[files[logEvent.FileId].ServerId];
                    if (!dimServers.TryGetValue(server.Name, out var dimServer))
                    {
                        dimServer = new DimServer(server.Name);
                        dimServers.Add(dimServer.ServerName, dimServer);
                        _dimServerRepository.Add(dimServer);
                    }

                    if (!dimHttpMethods.TryGetValue(logEvent.Method, out var dimHttpMethod))
                    {
                        dimHttpMethod = new DimHttpMethod(logEvent.Method);
                        dimHttpMethods.Add(dimHttpMethod.HttpMethod, dimHttpMethod);
                        _dimHttpMethodRepository.Add(dimHttpMethod);
                    }

                    if (!dimClientIps.TryGetValue(logEvent.ClientIpAddress, out var dimClientIp))
                    {
                        dimClientIp = new DimClientIp(logEvent.ClientIpAddress);
                        dimClientIps.Add(dimClientIp.ClientIpAddress, dimClientIp);
                        _dimClientIpRepository.Add(dimClientIp);
                    }

                    var dimDate = dimDates[logEvent.DateTime.Date];
                    var dimTime = dimTimes[logEvent.DateTime.TimeOfDay];

                    var factEvent = new FactEvent
                    {
                        AgentKey = dimAgent.AgentKey,
                        BytesReceived = logEvent.BytesReceived,
                        BytesSent = logEvent.BytesSent,
                        ClientIpKey = dimClientIp.ClientIpKey,
                        DateKey = dimDate.DateKey,
                        TimeTaken = logEvent.TimeTaken,
                        TimeKey = dimTime.TimeKey,
                        HttpMethodKey = dimHttpMethod.HttpMethodKey,
                        PortKey = dimPort.PortKey,
                        ServerIpKey = dimServerIp.ServerIpKey,
                        ServerKey = dimServer.ServerKey,
                        StatusKey = dimStatus.StatusKey,
                        SubStatusKey = dimSubStatus.SubStatusKey,
                        UriStem = logEvent.UriStem,
                        UriQuery = logEvent.UriQuery,
                        UserNameKey = dimUsername.UsernameKey,
                        Win32StatusKey = dimWin32Status.Win32StatusKey,
                        ReferenceId = logEvent.Id
                    };

                    toInsert.Add(factEvent);

                    logEvent.Processed = true;
                }

                if (toInsert.Any())
                {
                    _logger.Log($"Saving {toInsert.Count}");
                    _factEventRepository.BulkAdd(toInsert);

                    _logger.Log("Updating Processed Flag");
                    _logEventRepository.UpdateAll(work);
                    toInsert.Clear();
                }
            }
        }

        private bool GetWork(out List<LogEvent> events)
        {
            events = _logEventRepository.LoadTopNotProcessed(100000);
            return events.Any();
        }
    }
}
