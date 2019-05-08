using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;
using System.Collections.Generic;

namespace GrammerTest.Grammer.AstTreeBuilders
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
