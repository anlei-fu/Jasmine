using Jasmine.Spider.Grammer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class CallBuilder : BuilderBase
    {
        public CallBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public CallNode Build()
        {
            return null;
        }

    }
}
