using GrammerTest.Grammer.AstTreeBuilders;
using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public  class IfBlockBuilder:BuilderBase
    {
        public IfBlockBuilder(ISequenceReader<Token> reader, Block block) : base(reader, block)
        {
        }

        public override string Name => "IfBuilder";

        public IfBlock Build()
        {

            var ifBlock = new IfBlock(_block);

            var if0Block = new If0BlockBuilder(_reader,ifBlock).Build();

            ifBlock.If0Blocks.Add(if0Block);

            while (_reader.HasNext())
            {
                _reader.Next();
                var token = _reader.Current();

                if(token.Value==Keywords.ELIF)
                {
                    ifBlock.If0Blocks.Add(new If0BlockBuilder(_reader,ifBlock).Build());
                }
                else if(token.Value==Keywords.ELSE)
                {

                    ifBlock.ElseBlock = new ElseBlockBuilder(_reader,ifBlock).Build();

                    return ifBlock;
                }
                else
                {
                    _reader.Back();

                    return ifBlock;
                }

            }

            return ifBlock;
        }
    }
}
