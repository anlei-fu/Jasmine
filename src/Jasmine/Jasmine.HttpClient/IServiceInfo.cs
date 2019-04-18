namespace Jasmine.HttpClient
{
    public  interface IServiceInfo
    {
        string Name { get; }
        Domain GetDomain();
        string Path { get; }
        int Method { get; }
        int RpcType { get; }
        int TimeOut { get; set; }
        int MaxRetryTime { get; }
    }
}
