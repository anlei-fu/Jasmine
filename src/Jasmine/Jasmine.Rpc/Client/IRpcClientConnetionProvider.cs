namespace Jasmine.Rpc.Client
{
    public  interface IRpcClientConnetionProvider
    {
        IRpcClientConnection Get(string group);
    }
}
