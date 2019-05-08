

using System.Collections.Generic;
using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;

namespace Jasmine.Spider.Grammer
{
    public class IfBlock: BreakableBlock
    {
        public List<If0Block> If0Blocks = new List<If0Block>();
        public ElseBlock ElseBlock { get; set; }

       private bool _isMatchFound;

        public IfBlock(Block parent) : base(parent)
        {
        }

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

                if (_break)
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

        public override void Return(JObject result)
        {
            throw new System.NotImplementedException();
        }
    }
}
