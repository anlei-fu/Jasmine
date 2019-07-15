using System;

namespace Jasmine.HttpClient
{
    public interface IRestfulClientProvider
    {
       object Get(Type type);
    }
}
