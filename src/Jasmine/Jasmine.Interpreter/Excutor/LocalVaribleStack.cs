using System.Collections.Generic;

namespace Jasmine.Interpreter.Excutor
{
    public  class LocalVaribleStack
    {
        private List<LocalVaribleTable> _tables;

        public void PopBreak()
        {

        }
        public void PopBlock()
        {

        }

        public void Push(LocalVaribleTable table)
        {
            _tables.Add(table);
        }

        public LocalVaribleTable Current => _tables[_tables.Count - 1];
    }
}
