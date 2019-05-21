using System;

namespace Jasmine.Configuration
{
    public  interface IConfigrationProvider
    {
        string GetConfig(string config);
        T GetConfig<T>(string config);
        object GetConfig(Type type, string config);

    }
}
