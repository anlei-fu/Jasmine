using Jasmine.Interpreter.TypeSystem;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Jasmine.Interpreter.Scopes
{
    public class IfBlock: BreakableBlock
    {
        public IfBlock(BreakableBlock parent) : base(parent)
        {
        }
        private bool _isMatchFound;
        public List<If0Block> If0Blocks = new List<If0Block>();
        public ElseBlock ElseBlock { get; set; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMatchFound()
        {
            _isMatchFound = true;
        }

        private bool _break;
        public override void Excute()
        {

            foreach (var item in If0Blocks)
            {
                if (_isMatchFound)
                    break;

               

                item.Excute();

            }

            if (!_isMatchFound && ElseBlock != null)
                ElseBlock.Excute();

            _break = false;
            _isMatchFound = false;
        }

        public override void Break()
        {
            ((BreakableBlock)Parent).Break();

            _break = true;
        }

        public override void Continue()
        {
            ((BreakableBlock)Parent).Continue();

            _break = true;
        }

        public override void Catch(JError error)
        {
            throw new System.NotImplementedException();
        }

        public override void Return(Any result)
        {
            throw new System.NotImplementedException();
        }
    }
}
