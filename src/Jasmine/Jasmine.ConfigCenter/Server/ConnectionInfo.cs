using DotNetty.Transport.Channels;
using System.Collections.Generic;

namespace Jasmine.ConfigCenter.Server
{
    public class ConnectionInfo
    {
        public ConnectionInfo(IChannel channel)
        {
            Channel = channel;
        }
        private IDictionary<string,NodeEventManager> Managers;
        public IList<string> NodeOwn { get; set; }
        public IChannel Channel { get;  }
    }
}
