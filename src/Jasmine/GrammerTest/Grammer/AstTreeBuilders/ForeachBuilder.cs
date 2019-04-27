using GrammerTest.Grammer.AstTreeBuilders;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class ForeachBuilder : BuilderBase
    {
        public ForeachBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public ForeachBlock Build()
        {
            if(_reader.HasNext())
            {
                var token = _reader.Next();

                if(token.OperatorType==OperatorType.LeftParenthesis)
                {
                    if(_reader.HasNext()&&_reader.Next().TokenType==TokenType.Identifier)
                    {
                        if(_reader.HasNext()&&_reader.Next().Value=="in")
                        {

                            var expression = new AstNodeBuilder(_reader,  null).Build();

                            var scope = new BlockBuilder(_reader).Build();

                        }
                        else
                        {

                        }


                    }
                    else
                    {

                    }


                }
                else
                {
                    throwError("");
                }

            }
            else
            {

            }





            return null;
        }
    }
}
