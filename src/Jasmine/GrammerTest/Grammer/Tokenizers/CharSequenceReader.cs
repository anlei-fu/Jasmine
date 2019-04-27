using System.Collections.Generic;

namespace GrammerTest.Grammer.Tokenizers
{
    public class CharSequenceReader : SequenceReader<char>
    {
        public CharSequenceReader(IEnumerable<char> sequence) : base(sequence)
        {
        }
        private IList<int> _lineNumbers = new List<int>();
        public int Line { get; private set; } = 1;
        public int LineNumber { get; private set; } = 1;

        public override char Next(int step = 1)
        {
            for (int i = 1; i <=step; i++)
            {
                if(Forward(i)=='\n')
                {
                    Line++;
                    _lineNumbers.Add(LineNumber);
                    LineNumber = 1;
                }
            }

            return base.Next(step);
        }

        public override char Previouce(int step = 1)
        {
            for (int i = 1; i <=step; i++)
            {
                if (Last(i) == '\n')
                {
                    Line--;
                    _lineNumbers.RemoveAt(_lineNumbers.Count-1);
                    LineNumber = _lineNumbers[_lineNumbers.Count-1];
                }
            }


            return base.Previouce(step);
        }
    }
}
