namespace Jasmine.ConfigCenter.Client
{
    public  interface IConfigCenterClient
    {
        void Connect(string host,string user);
        bool SubscribeDataChanged(string path);
        bool UnSubscribeDataChanged(string path);
        bool SubscribeChildrenCreated(string path);
        bool UnsubscribeChildrenCreated(string path);
        bool SubscribeNodeRemoved(string path);
        bool UnsubscribeNodeRemoved(string path);
        bool NodeExists(string path);
        string[] GetChildren(string path);
        byte[] GetData(string path);
        byte[] SetData(string path, byte[] data);
        void Close();
    }
}
