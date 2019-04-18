namespace Jasmine.Rpc
{
    public interface IRpcResponse
    {
        long Id { get; }
        ResponseErrorCode ErrorCode { get; set; }
        string Message { get; set; }
        byte[] Data { get; set; }

    }
}
