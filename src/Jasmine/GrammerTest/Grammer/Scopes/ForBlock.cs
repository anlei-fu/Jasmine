using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;

namespace Jasmine.Spider.Grammer
{
    public  class ForBlock:BodyBlock
    {
        public ForBlock(Block parent) : base(parent)
        {
            OperateExpression = new Expression(this);
        }

  
      
        public DeclareExpression DeclareExpression { get; set; }
        public Expression CheckExpression { get; set; }
        public Expression OperateExpression { get; set; }

        private bool _break;
       

        public override void Break()
        {
            _break = true;
        }

        public override void Catch(JError error)
        {
            throw new System.NotImplementedException();
        }

        public override void Continue()
        {
            
        }

        public override void Excute()
        {
            DeclareExpression.Excute();

            var t = 0;

            while(true)
            {
                if (_break)
                    break;


                CheckExpression.Excute();

                ++t;
                if (!(bool)CheckExpression.Root.Output)
                {
                    break;
                }

                Body.Excute();

                Body.UnsetAll();
                OperateExpression.Excute();

            }

            _break = false;
        }

        public override void Return(JObject result)
        {
            throw new System.NotImplementedException();
        }
    }
}
