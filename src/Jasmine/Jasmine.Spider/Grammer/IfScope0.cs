namespace Jasmine.Spider.Grammer
{
    public  class IfScope0 :BreakableScope
    {
        public new IfScope Parent;

        private AstTree matchTree;
        public bool IsMatch()
        {
            return true;
        }

        public override void Excute()
        {
            if(IsMatch())
            {
                Parent.SetMatchFound();

                base.Excute();
            }
        }

      
    }
}
