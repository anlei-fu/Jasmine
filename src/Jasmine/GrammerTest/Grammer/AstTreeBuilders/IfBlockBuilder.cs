using GrammerTest.Grammer.AstTreeBuilders;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public  class IfBlockBuilder:BuilderBase
    {
        public IfBlockBuilder(ISequenceReader<Token> reader) : base(reader)
        {
        }

        public override string Name => "IfBuilder";

        public IfBlock Build()
        {

            var ifBlock = new IfBlock();

            var if0Block = new If0BlockBuilder(_reader).Build();

            ifBlock.If0Blocks.Add(if0Block);

            while (_reader.HasNext())
            {
                _reader.Next();
                var token = _reader.Current();

                if(token.Value==Keywords.ELIF)
                {
                    ifBlock.If0Blocks.Add(new If0BlockBuilder(_reader).Build());
                }
                else if(token.Value==Keywords.ELSE)
                {

                    ifBlock.ElseBlock = new ElseBlockBuilder(_reader).Build();

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
