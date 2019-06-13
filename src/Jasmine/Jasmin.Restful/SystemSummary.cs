

using System.Runtime.InteropServices;

namespace Jasmine.Restful
{
    public  class SystemSummary
    {
        

        public static SystemSummary GetSummary()
        {
            
            return null;
        }
        public float CupUsage { get; set; }
        public long MemoryUsage { get; set; }
        public long MemoryTotal { get; set; }
        public string RunningTime { get; set; }
        public string TotalVisteCount { get; set; }
        public string ListenPort { get; set; }
        public string SystemVersion => RuntimeEnvironment.GetSystemVersion();
        public string FailedCount { get; set; }
        public string ApplicationVersion { get; set; } = "Jasmine restful application v1.0.0.0";
        public string Archtecture =>RuntimeInformation.OSArchitecture.ToString();
    }
}
