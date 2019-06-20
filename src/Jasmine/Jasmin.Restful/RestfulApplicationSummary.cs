

using System.Runtime.InteropServices;

namespace Jasmine.Restful
{
    public  class RestfulApplicationSummary
    {
        

        public static RestfulApplicationSummary GetSummary()
        {
            
            return null;
        }
      
        public int TotalServiceCount { get; set; }
        public int TotalServiceGroupCount { get; set; }
        public string TotalVisiteCount { get; set; }
        public string TotalFailedCount { get; set; }
        public float CupUsage { get; set; }
        public long MemoryUsage { get; set; }
        public long MemoryTotal { get; set; }
        public string StartTimeTime { get; set; }
        public string SystemVersion => RuntimeEnvironment.GetSystemVersion();
        public string ApplicationVersion => "Jasmine restful application v1.0.0.0";
        public string Archtecture =>RuntimeInformation.OSArchitecture.ToString();
    }
}
