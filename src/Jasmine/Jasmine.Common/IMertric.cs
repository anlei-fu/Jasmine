namespace Jasmine.Common
{
    public  interface IMetric
      
    {
        int Avarage { get; }
        int Total { get; }
        int Success { get; }
        int Failed { get; }
        int Fastest { get; }
        int Slowest { get; }
        float FaileRate { get; }
        float SuccesRate { get; }
        int Median { get; }

        string LastCaculateTime { get; }
        void Add(IStatItem item);
        void Caculate();
        void Clear();
    }
}
