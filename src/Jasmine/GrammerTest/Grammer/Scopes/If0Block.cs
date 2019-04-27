namespace Jasmine.Spider.Grammer
{
    public  class If0Block :BreakableBlock
    {
        public new IfBlock Parent { get; set; }

        public Expression CheckExpression { get; set; }
      
        public bool IsMatch()
        {
            CheckExpression.Excute();

            return (bool)CheckExpression.Root.Output;
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
