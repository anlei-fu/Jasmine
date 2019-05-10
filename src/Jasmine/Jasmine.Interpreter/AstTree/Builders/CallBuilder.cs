using Jasmine.Interpreter.Scopes;
using Jasmine.Interpreter.Tokenizers;
using System.Collections.Generic;

namespace Jasmine.Interpreter.AstTree.Builders
{
    public class CallBuilder : BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            ",",")"
        };

        public CallBuilder(ISequenceReader<Token> reader, BreakableBlock block) : base(reader, block)
        {
        }
        public override string Name => "CallBuilder";
        public CallNode Build()
        {
            var callNode =  OperatorNodeFactory.CreateCall(_block);

            IList<OperatorNode> parameters = new List<OperatorNode>();

            bool isParameterCompleted = false;

            while (_reader.HasNext())
            {
                var node = new AstNodeBuilder(_reader,_block, _interceptChars).Build();

                if (node != null)
                    parameters.Add(node);

                if (_reader.Current().OperatorType == OperatorType.RightParenthesis)
                {
                    isParameterCompleted=true;

                    break;
                }
            }

            if (!isParameterCompleted)
                throwError("");

            callNode.Operands.AddRange(parameters);

            return callNode;

        }

    }
}
