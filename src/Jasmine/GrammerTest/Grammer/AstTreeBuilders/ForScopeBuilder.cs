using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public  class ForScopeBuilder:BuilderBase
    {
        public ForScopeBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public  ForBlock Build()
        {
            if(_reader.HasNext())
            {
                var token = _reader.Next();
                if(token.IsOperator()&&token.OperatorType==OperatorType.LeftParenthesis)
                {
                    var declare = new ExpressionBuilder(_reader).Build();

                    var judge = new ExpressionBuilder(_reader).Build();
                    var increment = new ExpressionBuilder(_reader).Build();

                    if (_reader.HasNext())
                    {
                        token = _reader.Next();

                        if(token.OperatorType==OperatorType.RightParenthesis)
                        {

                        }
                        else
                        {
                            throwError("");
                        }

                    }
                    else
                    {
                        throwError("");
                    }


                }
                else
                {
                    throwError("");
                }
            }
            else
            {
                throwError("");
            }

            return null;
        }
    }
}
