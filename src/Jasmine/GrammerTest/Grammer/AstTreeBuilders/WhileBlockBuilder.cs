using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class WhileBlockBuilder : BuilderBase
    {
        public WhileBlockBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public WhileBlock Build()
        {
            return null;
        }
    }
}
