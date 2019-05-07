using GrammerTest.Grammer.AstTreeBuilders;
using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;
using System.Collections.Generic;

namespace GrammerTest.Grammer
{
    public class FunctionBuilder : BuilderBase
    {
        public FunctionBuilder(ISequenceReader<Token> reader, Block block) : base(reader, block)
        {
        }

        public override string Name =>"FunctionBuilder";

        public JFunction Build()
        {

            var func = new JFunction();

            if(_reader.HasNext())
            {
                //get name
                _reader.Next();

                var token = _reader.Current();

                throwIf(x =>!x.IsIdentifier());

                func.Name = token.Value;

                throwErrorIfHasNoNextAndNext("incompleted function define;");

                throwErrorIfOperatorTypeNotMatch(OperatorType.LeftParenthesis);

                bool isParameterDefineFinished = false;

                var parameters = new List<string>();

                //resolve parameters

                while(_reader.HasNext())
                {
                    _reader.Next();

                    token = _reader.Current();

                    if(token.IsIdentifier())
                    {
                        if (_reader.Last().IsIdentifier())
                            throwUnexceptedError();

                        parameters.Add(token.Value);
                    }
                    else if(token.OperatorType==OperatorType.Coma)
                    {
                        if (!_reader.Last().IsIdentifier())
                            throwUnexceptedError();
                    }
                    else if(token.OperatorType==OperatorType.RightParenthesis)
                    {
                        if (_reader.Last().OperatorType == OperatorType.LeftParenthesis
                                                                 || _reader.Last().IsIdentifier())
                        {

                            isParameterDefineFinished = true;

                            break;
                        }

                        throwUnexceptedError();


                    }
                    else
                    {
                        throwUnexceptedError();
                    }
                }


                //check paramter is finished
                if (!isParameterDefineFinished)
                    throwError("incompleted function define;");

                //resolve function -body
                func.Body = new OrderedBlockBuilder(_reader,"function",_block).Build();

                return func;
            }
            else
            {
                throwError("incompleted function define;");
            }


            return null;
        }
    }
}
