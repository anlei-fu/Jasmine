using Jasmine.Spider.Grammer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammerTest.Grammer
{
    public class ElseBlockBuilder : BuilderBase
    {
        public ElseBlockBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public ElseBlock Build()
        {
            return null;
        }
    }
}
