using DotNetty.Transport.Channels;
using Jasmine.ConfigCenter.Common;
using System.Collections.Generic;

namespace Jasmine.ConfigCenter.Server
{
    public class ConnectionInfo
    {
        public ConnectionInfo(IChannel channel)
        {
            Channel = channel;
        }
       
    }
}
