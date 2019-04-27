using System;
using System.Collections.Generic;
using System.Linq;

namespace GrammerTest.Grammer.Tokenizers
{
    public abstract class AbstractSequenceReader<T> : ISequenceReader<T>
    {
        public AbstractSequenceReader(IEnumerable<T> sequence)
        {
            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));

            _sequence = sequence.ToArray();
        }

        protected T[] _sequence;
        public int Total => _sequence.Length;
        public int Readed { get; protected set; } = -1;
        public int Remain => Total-Readed;
        public abstract T Current();
        public abstract T Forward(int step = 1);
        public abstract bool HasNext(int step = 1);
        public abstract bool HasPreviouce(int step = 1);
        public abstract T Last(int step = 1);
        public abstract T Next(int step = 1);
        public abstract T Previouce(int step = 1);
       
    }
}
