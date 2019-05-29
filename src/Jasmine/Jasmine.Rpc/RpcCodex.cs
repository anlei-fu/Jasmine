using Jasmine.Extensions;
using Jasmine.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Rpc.Server
{
    public class RpcRequestResponseEncoder
    {
        public RpcRequestResponseEncoder(ISerializer serializer)
        {
            _serializer = serializer;
        }

        private ISerializer _serializer;

        public byte[] EncodeClientRequest(RpcRequest request)
        {
            var builder = new BytesBuilder(_serializer);

            builder.AppendLong(request.RequestId);
            builder.Append(request.Path);
            builder.Append(request.Body);

            return builder.Build();

        }

        public byte[] EncodeServerResponse(RpcResponse response)
        {
            var builder = new BytesBuilder(_serializer);

            builder.AppendLong(response.RequestId);
            builder.AppendInt(response.StatuCode);

            builder.Append(response.Body);

            return builder.Build();

        }
    }

    public class BytesBuilder
    {
        public BytesBuilder(ISerializer serializer, bool isHeartBeat = false)
        {
            _innerList.Add(isHeartBeat ? HEARTBEAT : REQUEST);
            _serializer = serializer;
        }
        private const byte HEARTBEAT = 5;
        private const byte REQUEST = 6;
        private ISerializer _serializer;
        private List<byte> _innerList = new List<byte>();

        public BytesBuilder Append(byte[] buffer)
        {
            AppendInt(buffer.Length);

            _innerList.AddRange(buffer);
            return this;
        }

        public BytesBuilder Append(string str)
        {
            return Append(Encoding.UTF8.GetBytes(str));
        }

        public byte[] Build()
        {
            return _innerList.ToArray();
        }

        public BytesBuilder AppendInt(int value)
        {
            _innerList.AddRange(BitConverter.GetBytes(value));
            return this;
        }
        public BytesBuilder AppendLong(long value)
        {
            _innerList.AddRange(BitConverter.GetBytes(value));
            return this;
        }

    }



}
