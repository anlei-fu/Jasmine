using System.Collections.Generic;

namespace Jasmine.Interpreter.Tokenizers
{
    public class CharSequenceReader : SequenceReader<char>
    {
        public CharSequenceReader(IEnumerable<char> sequence=null) : base(sequence)
        {
        }
        private IList<int> _lineNumbers = new List<int>();
        public int Line { get; private set; } = 1;
        public int LineNumber { get; private set; } = 1;

        public override void Next(int step = 1)
        {
            for (int i = 1; i <=step; i++)
            {
                if(Forwards(i)=='\n')
                {
                    Line++;
                    _lineNumbers.Add(LineNumber);
                    LineNumber = 1;
                }
            }

            base.Next();
            
        }

        public override void Back(int step = 1)
        {
            for (int i = 1; i <=step; i++)
            {
                if (Last(i) == '\n')
                {
                    Line--;
                    _lineNumbers.RemoveAt(_lineNumbers.Count-1);
                    LineNumber = _lineNumbers.Count==0?0:_lineNumbers[_lineNumbers.Count-1];
                }
            }

            base.Back();

        }
    }
}
