using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Jasmine.Extensions
{
    public class InsufficientException:Exception
    {
        public InsufficientException(int desire,int real):base($"desire to read {desire}, but actually read {real}")
        {

        }
    }
    /// <summary>
    /// don't know  is inline useful in extension methods
    /// </summary>
    public static  class StreamExtensions
    {
        #region Nomal Write...............

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteSByte(this Stream stream, sbyte value)
        {
            stream.WriteByte0((byte)value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteByte0(this Stream stream, byte value)
        {
            stream.WriteByte(value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUShort(this Stream stream,ushort value)
        {
             WriteShort(stream, (short)value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static  Task WriteUshortAsync(this Stream stream,ushort value)
        {
            return WriteShortAsync(stream, (short)value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteShort(this Stream stream, short value)
        {
            stream.WriteBytes(BitConverter.GetBytes(value));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WriteShortAsync(this Stream stream, short value)
        {
            return stream.WriteBytesAsync(BitConverter.GetBytes(value));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUInt(this Stream stream, uint value)
        {
            WriteInt(stream, (int)value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static  Task WriteUIntAsync(this Stream stream, uint value)
        {
            return WriteIntAsync(stream, (int)value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt(this Stream stream,int value)
        {
            WriteBytes(stream, BitConverter.GetBytes(value));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static  Task WriteIntAsync(this Stream stream,int value)
        {
            return WriteBytesAsync(stream, BitConverter.GetBytes(value));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteFloat(this Stream stream,float value)
        {
            WriteBytes(stream, BitConverter.GetBytes(value));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static  Task WriteFloatAsync(this Stream stream,float value)
        {
            return WriteBytesAsync(stream, BitConverter.GetBytes(value));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteDecimal(this Stream stream,decimal value)
        {
            WriteBytes(stream, BitConverter.GetBytes((double)value));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static  Task WriteDecimalAsync(this Stream stream,decimal value)
        {
            return WriteBytesAsync(stream, BitConverter.GetBytes((double)value));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteULong(this Stream stream, ulong value)
        {
            WriteLong(stream, (long)value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WriteULongAsync(this Stream stream, ulong value)
        {
            return WriteLongAsync(stream, (long)value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLong(this Stream stream,long value)
        {
            WriteBytes(stream, BitConverter.GetBytes(value));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WriteLongAsync(this Stream stream,long value)
        {
            return WriteBytesAsync(stream, BitConverter.GetBytes(value));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteDoble(this Stream stream,double value)
        {
            WriteBytes(stream, BitConverter.GetBytes(value));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static  Task WriteDoubleAsync(this Stream stream,double value)
        {
            return WriteBytesAsync(stream, BitConverter.GetBytes(value));
        }







        #endregion

        #region BigEndian Write........
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteSByteB(this Stream stream, sbyte value)
        {
            stream.WriteByte0((byte)value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteByteB0(this Stream stream, byte value)
        {
            stream.WriteByte(value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUShortB(this Stream stream, ushort value)
        {
            WriteShortB(stream, (short)value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WriteUshortBAsync(this Stream stream, ushort value)
        {
            return WriteShortBAsync(stream, (short)value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteShortB(this Stream stream, short value)
        {
            stream.WriteBytes(BitConverter.GetBytes(value).Reverse0());
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WriteShortBAsync(this Stream stream, short value)
        {
            return stream.WriteBytesAsync(BitConverter.GetBytes(value).Reverse0());
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUIntB(this Stream stream, uint value)
        {
            WriteIntB(stream, (int)value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WriteUIntBAsync(this Stream stream, uint value)
        {
            return WriteIntBAsync(stream, (int)value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteIntB(this Stream stream, int value)
        {
            WriteBytes(stream, BitConverter.GetBytes(value).Reverse0());
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WriteIntBAsync(this Stream stream, int value)
        {
            return WriteBytesAsync(stream, BitConverter.GetBytes(value).Reverse0());
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteFloatB(this Stream stream, float value)
        {
            WriteBytes(stream, BitConverter.GetBytes(value).Reverse0());
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WriteFloatBAsync(this Stream stream, float value)
        {
            return WriteBytesAsync(stream, BitConverter.GetBytes(value).Reverse0());
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteDecimalB(this Stream stream, decimal value)
        {
            WriteBytes(stream, BitConverter.GetBytes((double)value).Reverse0());
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WriteDecimalBAsync(this Stream stream, decimal value)
        {
            return WriteBytesAsync(stream, BitConverter.GetBytes((double)value).Reverse0());
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteULongB(this Stream stream, ulong value)
        {
            WriteLongB(stream, (long)value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WriteULongBAsync(this Stream stream, ulong value)
        {
            return WriteLongBAsync(stream, (long)value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLongB(this Stream stream, long value)
        {
            WriteBytes(stream, BitConverter.GetBytes(value).Reverse0());
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WriteLongBAsync(this Stream stream, long value)
        {
            return WriteBytesAsync(stream, BitConverter.GetBytes(value).Reverse0());
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteDobleB(this Stream stream, double value)
        {
            WriteBytes(stream, BitConverter.GetBytes(value).Reverse0());
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WriteDoubleBAsync(this Stream stream, double value)
        {
            return WriteBytesAsync(stream, BitConverter.GetBytes(value).Reverse0());
        }



        #endregion


        #region Read Normal....

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReadUShort(this Stream stream)
        {
            var pair = ReadKOrThrow(stream, 2);

            return BitConverter.ToUInt16(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<ushort> ReadUshortAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 2);

            return BitConverter.ToUInt16(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReadShort(this Stream stream)
        {
            var pair = ReadKOrThrow(stream, 2);

            return BitConverter.ToInt16(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<short> ReadShortAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 2);

            return BitConverter.ToInt16(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReadUInt(this Stream stream)
        {
            var pair = ReadKOrThrow(stream, 4);

            return BitConverter.ToUInt32(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<uint> ReadUIntAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 4);

            return BitConverter.ToUInt32(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadInt(this Stream stream)
        {
            var pair = ReadKOrThrow(stream, 4);

            return BitConverter.ToInt32(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<int> ReadIntAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 4);

            return BitConverter.ToInt32(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ReadFloat(this Stream stream)
        {
            var pair =  ReadKOrThrow(stream, 4);

            return BitConverter.ToSingle(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<float> ReadFloatAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 4);

            return BitConverter.ToSingle(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal ReadDecimal(this Stream stream)
        {
            return (decimal)ReadDoble(stream);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<decimal> ReadDecimalAsync(this Stream stream)
        {
            return (decimal) await ReadDoubleAsync(stream);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReadULong(this Stream stream)
        {
            var pair = ReadKOrThrow(stream, 8);

            return BitConverter.ToUInt64(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<ulong> ReadULongAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 8);

            return BitConverter.ToUInt64(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ReadLong(this Stream stream)
        {
            var pair = ReadKOrThrow(stream, 8);

            return BitConverter.ToInt64(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<long> ReadLongAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 8);

            return BitConverter.ToInt64(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ReadDoble(this Stream stream)
        {
            var pair =  ReadKOrThrow(stream, 8);

            return BitConverter.ToDouble(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<double> ReadDoubleAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 8);

            return BitConverter.ToDouble(pair.Buffer,0);
        }

        #endregion
        #region Read BigEndian
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReadUShortB(this Stream stream)
        {
            var pair = ReadKOrThrow(stream, 2);

            return BitConverter.ToUInt16(pair.Buffer.Reverse0(), 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<ushort> ReadUshortBAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 2);

            return BitConverter.ToUInt16(pair.Buffer.Reverse0(), 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReadShortB(this Stream stream)
        {

            var pair = ReadKOrThrow(stream, 2);

            return BitConverter.ToInt16(pair.Buffer.Reverse0(), 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<short> ReadShortBAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 2);

            return BitConverter.ToInt16(pair.Buffer.Reverse0(), 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReadUIntB(this Stream stream)
        {
            var pair = ReadKOrThrow(stream, 4);

            return BitConverter.ToUInt32(pair.Buffer.Reverse0(), 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<uint> ReadUIntBAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 4);

            return BitConverter.ToUInt32(pair.Buffer.Reverse0(), 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadIntB(this Stream stream)
        {
            var pair = ReadKOrThrow(stream, 4);

            return BitConverter.ToInt32(pair.Buffer.Reverse0(), 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<int> ReadIntBAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 4);

            return BitConverter.ToInt32(pair.Buffer.Reverse0(), 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ReadFloatB(this Stream stream)
        {
            var pair = ReadKOrThrow(stream, 4);

            return BitConverter.ToSingle(pair.Buffer.Reverse0(), 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<float> ReadFloatBAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 4);

            return BitConverter.ToSingle(pair.Buffer.Reverse0(), 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal ReadDecimalB(this Stream stream)
        {
            return (decimal)ReadDobleB(stream);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<decimal> ReadDecimalBAsync(this Stream stream)
        {
            return (decimal)await ReadDoubleBAsync(stream);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReadULongB(this Stream stream)
        {
            var pair = ReadKOrThrow(stream, 8);

            return BitConverter.ToUInt64(pair.Buffer.Reverse0(), 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<ulong> ReadULongBAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 8);

            return BitConverter.ToUInt64(pair.Buffer.Reverse0(), 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ReadLongB(this Stream stream)
        {
            var pair = ReadKOrThrow(stream, 8);

            return BitConverter.ToInt64(pair.Buffer.Reverse0(), 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<long> ReadLongBAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 8);

            return BitConverter.ToInt64(pair.Buffer.Reverse0(), 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ReadDobleB(this Stream stream)
        {
            var pair = ReadKOrThrow(stream, 8);

            return BitConverter.ToDouble(pair.Buffer, 0);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<double> ReadDoubleBAsync(this Stream stream)
        {
            var pair = await ReadKOrThrowAsync(stream, 8);

            return BitConverter.ToDouble(pair.Buffer.Reverse0(), 0);
        }
        #endregion

        public static string ReadString(this Stream stream,int length,Encoding encoding=null)
        {
            var pair = ReadKOrThrow(stream, length);

            encoding = encoding ?? Encoding.UTF8;

            return encoding.GetString(pair.Buffer);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<string> ReadStringAsync(this Stream stream, int length, Encoding encoding = null)
        {
            var pair = await ReadKOrThrowAsync(stream, length);

            encoding = encoding ?? Encoding.UTF8;

            return encoding.GetString(pair.Buffer);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BytesReadPair Read(this Stream stream, int length)
        {
            BytesReadPair pair = new BytesReadPair()
            {
                Buffer = new byte[length],
            };

            var real = stream.Read(pair.Buffer, 0, length);

            pair.Length = real;

            return pair;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BytesReadPair ReadKOrThrow(this Stream stream, int length)
        {
            var pair = Read(stream, length);

            if (pair.Length < length)
                throw new InsufficientException(length, pair.Length);

            return pair;

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<BytesReadPair> ReadKOrThrowAsync(this Stream stream, int length)
        {
            var pair = await ReadAsync(stream, length);

            if (pair.Length < length)
                throw new  InsufficientException (length,pair.Length);

            return pair;

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<BytesReadPair> ReadAsync(this Stream stream, int length)
        {
            BytesReadPair pair = new BytesReadPair()
            {
                Buffer = new byte[length],
            };

            var real = await stream.ReadAsync(pair.Buffer, 0, length).ConfigureAwait(false);

            pair.Length = real;

            return pair;

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteString(this Stream stream, string input, Encoding encoding = null)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            encoding = encoding ?? Encoding.UTF8;

            WriteBytes(stream, encoding.GetBytes(input));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WriteStringAsync(this Stream stream, string input, Encoding encoding = null)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            encoding = encoding ?? Encoding.UTF8;

            return WriteBytesAsync(stream, encoding.GetBytes(input));

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteBytes(this Stream stream, byte[] buffer)
        {
            stream.Write(buffer, 0, buffer.Length);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task WriteBytesAsync(this Stream stream, byte[] buffer)
        {
            return stream.WriteAsync(buffer, 0, buffer.Length);
        }
        public struct BytesReadPair
        {
            public int Length;
            public byte[] Buffer;
        }
    }
}
