using System;

namespace Jasmine.ConfigCenter.Client
{
    public abstract class AbstractConfigCenterClient : IConfigCenterClient
    {
        public abstract void Close();

        public abstract void Connect(string host, string user);
      
        public string[] GetChildren(string path)
        {
            throw new NotImplementedException();
        }

        public byte[] GetData(string path)
        {
            throw new NotImplementedException();
        }

        public bool NodeExists(string path)
        {
            throw new NotImplementedException();
        }

        public byte[] SetData(string path, byte[] data)
        {
            throw new NotImplementedException();
        }

        public bool SubscribeChildrenCreated(string path)
        {
            throw new NotImplementedException();
        }

        public bool SubscribeDataChanged(string path)
        {
            throw new NotImplementedException();
        }

        public bool SubscribeNodeRemoved(string path)
        {
            throw new NotImplementedException();
        }

        public bool UnsubscribeChildrenCreated(string path)
        {
            throw new NotImplementedException();
        }

        public bool UnSubscribeDataChanged(string path)
        {
            throw new NotImplementedException();
        }

        public bool UnsubscribeNodeRemoved(string path)
        {
            throw new NotImplementedException();
        }

      
       
    }
}
