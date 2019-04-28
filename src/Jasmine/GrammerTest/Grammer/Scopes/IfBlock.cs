

using System.Collections.Generic;
using GrammerTest.Grammer.TypeSystem;

namespace Jasmine.Spider.Grammer
{
    public class IfBlock: BreakableBlock
    {
        public List<If0Block> If0Blocks = new List<If0Block>();
        public ElseBlock ElseBlock { get; set; }

       private bool _isMatchFound;

        public void SetMatchFound()
        {
            _isMatchFound = true;
        }

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

        }

        public override void Break()
        {
            throw new System.NotImplementedException();
        }

        public override void Continue()
        {
            throw new System.NotImplementedException();
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
