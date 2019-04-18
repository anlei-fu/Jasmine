using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.ConfigCenter.Server
{
   public interface IConnectionManager
    {
        void RemoveConnection(string id);

        void AddConnection(string id, ConnectionInfo connection);
        ConnectionInfo GetConnection(string id);
        bool ConnectionRegisted(string id);
    }
}
