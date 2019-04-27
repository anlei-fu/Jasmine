using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GrammerTest.Grammer.Tokenizers
{
    public class SequenceReader<T> : AbstractSequenceReader<T>
    {
        public SequenceReader(IEnumerable<T> sequence) : base(sequence)
        {
        }

       
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T Current()
        {
            return _sequence[Readed];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T Forward(int step = 1)
        {
            return _sequence[Readed + step];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool HasNext(int step = 1)
        {
            return step + Readed < _sequence.Length;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool HasPreviouce(int step = 1)
        {
            return step + Readed > -1;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T Last(int step = 1)
        {
            return _sequence[Readed - step];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T Next(int step = 1)
        {
            Readed = Readed + step;

            return Current();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T Previouce(int step = 1)
        {
            Readed = Readed - step;

            return Current();
        }
    }
}
