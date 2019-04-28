using GrammerTest.Grammer.AstTreeBuilders;
using Jasmine.Spider.Grammer;
using System.Collections.Generic;

namespace GrammerTest.Grammer
{
    public class FunctionBuilder : BuilderBase
    {
        public FunctionBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public JFunction Build()
        {

            var func = new JFunction();

            if(_reader.HasNext())
            {
                //get name

                var token = _reader.Next();

                if (!token.IsIdentifier())
                    throwError("");

                func.Name = token.Value;


                if (!_reader.HasNext())
                    throwError("");

                if (_reader.Next().OperatorType != OperatorType.LeftParenthesis)
                    throwError("");

                bool isParameterDefineFinished = false;

                var parameters = new List<string>();

                //resolve parameters

                while(_reader.HasNext())
                {
                    token = _reader.Next();

                    if(token.IsIdentifier())
                    {
                        if (_reader.PreviouceToken().IsIdentifier())
                            throwError("");

                        parameters.Add(token.Value);
                    }
                    else if(token.OperatorType==OperatorType.Coma)
                    {
                        if (!_reader.PreviouceToken().IsIdentifier())
                            throwError("");
                    }
                    else if(token.OperatorType==OperatorType.RightParenthesis)
                    {
                        if (_reader.PreviouceToken().OperatorType == OperatorType.LeftParenthesis
                                                                 || _reader.PreviouceToken().IsIdentifier())
                        {

                            isParameterDefineFinished = true;

                            break;
                        }

                        throwError("");


                    }
                    else
                    {
                        throwError("");
                    }
                }


                //check paramter is finished
                if (!isParameterDefineFinished)
                    throwError("");

                //resolve function -body
                func.Body = new BlockBuilder(_reader).Build();

                return func;
            }
            else
            {
                throwError("");
            }


            return null;
        }
    }
}
