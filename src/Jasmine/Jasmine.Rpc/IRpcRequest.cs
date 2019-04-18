using System.Collections.Generic;

namespace Jasmine.Rpc
{
    public interface IRpcRequest
    {
        long Id { get; }
        string Group { get; }
        string Name { get; }
        IDictionary<string, object> Parameter { get; }
        int Timeout { get; }
       
    }
}
