using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class ExpressionBuilder:BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            ";"
        };
        public ExpressionBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public  Expression Build()
        {

            var expression = new Expression();

            expression.Root = new AstNodeBuilder(_reader, _interceptChars).Build();

            return expression;

        }

      


    }
}
