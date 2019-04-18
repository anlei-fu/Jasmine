using Jasmine.ConfigCenter.Common;
using System;

namespace Jasmine.ConfigCenter.Client
{
    public abstract class AbstractConfigCenterClient : IConfigCenterClient
    {
        public abstract void Close();
       

        public abstract bool Connect(string host, string user, string id);
       
        public byte[] GetData(string path)
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

        protected abstract ConfigCenterServiceResponse call(ConfigCenterServiceRequest request);
       
    }
}
