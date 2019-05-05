using System;
using System.Collections.Generic;
using System.Linq;

namespace GrammerTest.Grammer.Tokenizers
{
    public abstract class AbstractSequenceReader<T> : ISequenceReader<T>
    {
        public AbstractSequenceReader(IEnumerable<T> sequence = null)
        {

            if (sequence != null)
                Sequence = sequence.ToArray();
        }

        public T[] Sequence { get; private set; }
        public int Total => Sequence.Length;
        public int Readed { get; protected set; } = -1;
        public int Remain => Total - Readed;
        public abstract T Current();
        public abstract T Forwards(int step = 1);
        public abstract bool HasNext(int step = 1);
        public abstract bool HasPrevious(int step = 1);
        public abstract T Last(int step = 1);
        public abstract void Next(int step = 1);
        public abstract void Back(int step = 1);
        public void Reset(IEnumerable<T> input)
        {
            Sequence = input.ToArray();

            Readed = -1;
        }

    }
}
