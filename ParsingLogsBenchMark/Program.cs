using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using IISLogsToSqlServer;
using IISLogsToSqlServer.Parser.Interfaces;
using IISLogsToSqlServer.Parser.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ParsingLogsBenchMark
{
    class Program
    {
        static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<ParseBenchmark>();
            var parseBenchMark = new ParseBenchmark();
            parseBenchMark.Setup();
            parseBenchMark.Read();
        }
    }

    [SimpleJob(RunStrategy.Throughput, targetCount: 5)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class ParseBenchmark
    {
        private IIISEventLogReader _reader;

        [GlobalSetup]
        public void Setup()
        {
            var container = IoC.Initialize();
            _reader = container.GetService<IIISEventLogReader>();
        }

        [Benchmark]
        public List<LogEvent> Read()
        {
            using (var reader = File.OpenText(".\\Files\\u_ex181003.log"))
            {
               return _reader.Read(reader).ToList();
            }
        }
    }
}
