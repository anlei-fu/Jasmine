using GrammerTest.Grammer.AstTreeBuilders;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public  class IfBlockBuilder:BuilderBase
    {
       
        public IfBlockBuilder(TokenStreamReader reader) : base(reader)
        {
           
        }

        public IfBlock Build()
        {

            var ifBlock = new IfBlock();

            var if0Block = new If0BlockBuilder(_reader).Build();

            ifBlock.If0Blocks.Add(if0Block);

            while (_reader.HasNext())
            {
                var token = _reader.Next();

                if(token.Value==Keywords.ELIF)
                {
                    ifBlock.If0Blocks.Add(new If0BlockBuilder(_reader).Build());
                }
                else if(token.Value==Keywords.ELSE)
                {

                    ifBlock.ElseBlock = new ElseBlockBuilder(_reader).Build();

                    _reader.Previous();

                    return ifBlock;
                }
                else
                {
                    _reader.Previous();

                    return ifBlock;
                }

            }

            return ifBlock;
        }
    }
}
