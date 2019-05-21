using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Jasmine.Common
{
    public class SequenceReader<T> : AbstractSequenceReader<T>
    {
        public SequenceReader(IEnumerable<T> sequence) : base(sequence)
        {
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T Current()
        {
            return Sequence[Readed];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T Forwards(int step = 1)
        {
            return Sequence[Readed + step];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool HasNext(int step = 1)
        {
            return step + Readed < Sequence.Length;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool HasPrevious(int step = 1)
        {
            return step + Readed > 1;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T Last(int step = 1)
        {
            return Sequence[Readed - step];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Next(int step = 1)
        {
            Readed +=  step;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Back(int step = 1)
        {
            Readed -= step;
        }
    }
}
