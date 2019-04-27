using GrammerTest.Grammer;
using GrammerTest.Grammer.AstTreeBuilders;
using Jasmine.Spider.Grammer;

namespace GrammerTest
{
    public class DoWhileScopeBuilder : BuilderBase
    {
        public DoWhileScopeBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public DoWhileBlock Build()
        {
            var scope = new BlockBuilder(_reader).Build();

            if(_reader.HasNext())
            {
                if(_reader.Next().OperatorType==OperatorType.LeftParenthesis)
                {
                    var expression = new AstNodeBuilder(_reader,  null).Build();

                    if(_reader.HasNext())
                    {
                        if(_reader.Next().OperatorType==OperatorType.RightParenthesis)
                        {

                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }

                }
            }



            return null;
        }
    }
}
