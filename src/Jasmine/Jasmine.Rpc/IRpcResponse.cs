namespace Jasmine.Rpc
{
    public interface IRpcResponse
    {
        long Id { get; }
        int ErrorCode { get; set; }
        string Message { get; set; }
        byte[] Data { get; set; }

    }
}
