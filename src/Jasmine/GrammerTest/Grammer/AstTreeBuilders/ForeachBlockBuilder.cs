using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class ForeachBlockBuilder : BuilderBase
    {
        public ForeachBlockBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public ForeachBlock Build()
        {
            var foreachBlock = new ForeachBlock();


            if(_reader.HasNext())
            {
                var token = _reader.Next();

                if (token.OperatorType != OperatorType.LeftParenthesis)
                    throwError("");

                token = _reader.Next();

                if (token.Value != Keywords.VAR)
                    throwError("");

                token = _reader.Next();



            }
            else
            {

            }

            return null;
        }
    }
}
