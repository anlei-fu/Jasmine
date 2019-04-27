using System;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class ParameterBuilder :BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            ",",")"
        };
        public ParameterBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public OperatorNode Build()
        {
           return new AstNodeBuilder(_reader,  _interceptChars).Build();
        }
    }
}
