using Jasmine.Extensions;
using Jasmine.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Rpc.Server
{
    public class RpcCodex
    {
        public RpcCodex(ISerializer serializer)
        {
            _serializer = serializer;
        }
        private ISerializer _serializer;
        public RpcRequest DecodeServerRequest(byte[] buffer,int offset)
        {
            var reader = new BytesReader(buffer,offset);

            var request = new RpcRequest();

            request.RequestId = reader.ReadLong();

            request.Path = Encoding.UTF8.GetString(reader.ReadFiled());

            request.Path = request.Path.ToLower();

            var index = request.Path.IndexOf("?");

            if(index!=-1)
            {
                request.Query = getQuery(request.Path.Substring(index +1, request.Path.Length - index-1));
                request.Path = request.Path.Substring(0, index);
            }

            request.Body = reader.ReadFiled();

            return request;

        }


        private IDictionary<string,string> getQuery(string str)
        {
            var dic = new Dictionary<string, string>();
          
            foreach (var item in StringHelper.Splite1(str,"&"))
            {
                var pair = StringHelper.Splite1(item, "=");

                if(pair.Count==2)
                {
                    if (!dic.ContainsKey(pair[0]))
                        dic.Add(pair[0], pair[1]);
                }
            }

            return dic;
        }

        public byte[] EncodClientRequest(RpcRequest request)
        {
            var builder = new BytesBuilder(_serializer);

            builder.AppendLong(request.RequestId);
            builder.Append(request.Path);
            builder.Append(request.Body);

            return builder.Build();

        }


        public RpcResponse DecodeResponse(byte[] buffer,int offset)
        {
            var response = new RpcResponse();

            var reader = new BytesReader(buffer, offset);

            response.RequestId = reader.ReadLong();
            response.StatuCode = reader.ReadInt();
            response.Body = reader.ReadFiled();

            return response;


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

        public BytesBuilder(ISerializer serializer,bool isHeartBeat=false)
        {
            _innerList.Add(isHeartBeat?HEARTBEAT:REQUEST);
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
        public BytesBuilder Append(int length)
        {
            _innerList.AddRange(BitConverter.GetBytes(length));

            return this;
        }
      

        public BytesBuilder Append(string str)
        {
            return Append(Encoding.UTF8.GetBytes(str));
        }

        public BytesBuilder Append(object obj)
        {
            return Append(_serializer.SerializeToBytes(obj));
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


    public class BytesReader
    {

        public BytesReader(byte[] input,int offset)
        {
            _innerArray = input;
            _currentIndex = offset;
        }
        private readonly byte[] _lengthField = new byte[4];
        private byte[] _innerArray;
        private int _currentIndex;

        public int ReadInt()
        {
            var buffer = new byte[4];

            Buffer.BlockCopy(_innerArray, _currentIndex, buffer, 0, 4);

            _currentIndex += 4;

            return BitConverter.ToInt32(buffer, 0);
        }

        public long ReadLong()
        {
            var buffer = new byte[8];

            Buffer.BlockCopy(_innerArray, _currentIndex, buffer, 0, 8);

            _currentIndex += 8;

            return BitConverter.ToInt64(buffer, 0);
        }
        
        public byte[] ReadFiled()
        {
            Buffer.BlockCopy(_innerArray, _currentIndex, _lengthField, 0, 4);

            var length = BitConverter.ToInt32(_lengthField,0);

            _currentIndex += 4;

            var bytes = new byte[length];

            Buffer.BlockCopy(_innerArray, _currentIndex, bytes, 0, length);

            _currentIndex += length;

            return bytes;
        }
    }
}
