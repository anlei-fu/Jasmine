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
        private HashSet<INodeEventManager> Managers;
        public IChannel Channel { get;  }
    }
}
