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
        public CallBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public CallNode Build()
        {
            var callNode = new CallNode();

            IList<OperatorNode> parameters = new List<OperatorNode>();

            bool isParameterCompleted = false;

            while (_reader.HasNext())
            {
                var node = new AstNodeBuilder(_reader, _interceptChars).Build();

                if (node == null)
                    throwError("");

                if (_reader.CurrentToken.OperatorType == OperatorType.RightParenthesis)
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
