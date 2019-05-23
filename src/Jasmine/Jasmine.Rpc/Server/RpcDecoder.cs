using DotNetty.Buffers;
using Jasmine.Common;

namespace Jasmine.Rpc.Server
{
    public   class RpcDecoder
    {
       

        public RpcRequest Decode(byte[] buffer)
        {
            ISequenceReader<byte> _reader = new SequenceReader<byte>(buffer);

            try
            {
                _reader.Next();
            }
            catch
            {

            }

            return null;

        }
        public IByteBuffer Encode(RpcResponse response)
        {
            return null;
        }
    }
}
