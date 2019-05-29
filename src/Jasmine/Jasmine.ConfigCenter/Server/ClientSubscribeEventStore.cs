using DotNetty.Transport.Channels;
using Jasmine.ConfigCenter.Common;
using System.Collections.Generic;

namespace Jasmine.ConfigCenter.Server
{
    public class ClientSubscribeEventStore
    {
        private IDictionary<string, List<Event>> Events;
        public IList<string> NodeCreated { get; set; }
        public IChannel Channel { get; }

        public void Destroy()
        {

        }
    }
}
