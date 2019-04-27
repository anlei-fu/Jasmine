

using System.Collections.Generic;

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


    }
}
