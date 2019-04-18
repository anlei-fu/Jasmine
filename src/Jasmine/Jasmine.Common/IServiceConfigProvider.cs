namespace Jasmine.Common
{
    public interface IServiceConfigProvider
    {
        IService GetService(string name);
        IService GetService(string group, string name);
        IServiceGroup GetServiceGroup(string name);
    }
}
