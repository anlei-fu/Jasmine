using System.Collections.Generic;

namespace Jasmine.Restful.DefaultServices
{
    internal  class DashBoardService
    {


        public void GetSummary()
        {
            var all = new AllData();

            all.Summary = new Summary()
            {
                ServerConfig = RestfulApplicationGlobalConfig.ServerConfig,
            };

            all.Summary.AppConfig=new AppConfig()
            {

            };

        }
        public void GetAllData()
        {

        }

        public void StartGroup()
        {

        }
        public void StartService()
        {

        }

        public void StopGroup()
        {

        }
        public void StopService()
        {

        }


    }
    internal class AllData
    {
        public SytemApi[] SystemApis { get; set; } = SytemApi.Apis;
        public Summary Summary { get; set; }


    }

    internal class GroupStat
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartTime { get; set; }
        public bool Running { get; set; }
        public int TotalVisit { get; set; }
        public int FailedCount { get; set; }
        public int TimeTotal { get; set; }
        public float RateOfAll { get; set; }
        public PipelineDescription[] Pipeline { get; set; }
        public PipelineDescription[] Error { get; set; }
        public ServiceStat[] Services { get; set; }
    }

    internal class ServiceStat
    {
        public string Path { get; set; }
        public string Method { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
        public string StartTime { get; set; }
        public bool Running { get; set; }
        public int TotalVisit { get; set; }
        public int FailedCount { get; set; }
        public int TimeTotal { get; set; }
        public float RateofGroup { get; set; }
        public float RateOfAll { get; set; }
        public string ReturnValueExanple { get; set; }
        public ParameterDescription[] Parameters { get; set; }
        public PipelineDescription[] Pipeline { get; set; }
        public PipelineDescription[] Error { get; set; }
        public IEnumerable<RestfulTrace> Trace { get; set; }
 
    }


    internal class PipelineDescription
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    internal class ParameterDescription
    {
        public string Name { get; set; }
        public bool Required { get; set; }
        public string Location { get; set; }
        public string ExampleValue { get; set; }
    }
   internal  class Summary
    {
        public ServerConfig ServerConfig { get; set; }
        public AppConfig AppConfig { get; set; }
        public AppStat Stat { get; set; }

    }
    internal class AppConfig
    {
        public string Appversion { get; set; }
        public string Dispatcher { get; set; }
        public string Middleware { get; set; }
        public string TestFilePath { get; set; }
        public string DescriptionFilePath { get; set; }
        public string StaticFileEnabled { get; set; }
        public string VirtuRootPath { get; set; }
        public string MaxFileCacheMemoryUsage { get; set; }
        public string SystemAoiEnbaled { get; set; }
        public string EnableCrossDomain { get; set; }
        public string DebugMode { get; set; }

    }

    internal class AppStat
    {
        public string OperateSystem { get; set; }
        public string CpuArchitecture { get; set; }
        public string RunningTime { get; set; }
        public string CpuUsage { get; set; }
        public string MemoryUsage { get; set; }
        public string TotalMemory { get; set; }
        public int ServiceGroupCout { get; set; }
        public int ServiceCout { get; set; }
        public int TotalVisite { get; set; }
        public int TotalFailed { get; set; }
        public int TotalServeTime { get; set; }
    }

    internal class SytemApi
    {
        public static readonly SytemApi[] Apis = new SytemApi[]
        {

        };
        public string Path { get; set; }
        public string Method { get; set; }
        public string AuthenticateLevel { get; set; }
        public string Parameters { get; set; }
        public string Return { get; set; }
        public string Description { get; set; }
    

    }

  

}
