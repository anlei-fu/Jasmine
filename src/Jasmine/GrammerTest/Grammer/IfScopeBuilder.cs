using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
  public  class IfScopeBuilder:BuilderBase
    {
        public IfScopeBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public IfScope Build()
        {

            if(_reader.HasNext()&&_reader.Next().OperatorType==OperatorType.LeftParenn)
            {
                var node = new AstNodeBuilder(_reader, false, null).Build();


                if(node.OutputType!=OutputType.Bool)
                {
                    throwError("");
                }



            }


         


            return null;
        }
    }
}
