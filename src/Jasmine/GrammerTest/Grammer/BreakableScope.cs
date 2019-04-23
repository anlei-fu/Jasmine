namespace Jasmine.Spider.Grammer
{
    public  class BreakableScope:Scope
    {
        protected bool _isContinue;
        protected bool _isBreak;
        public virtual void Break()
        {
            if (Parent != null && Parent is BreakableScope)
            {
                ((BreakableScope)Parent).Break();
            }

            _isBreak = true;
        }
        public virtual void Continue()
        {
            if (Parent != null && Parent is BreakableScope)
            {
                ((BreakableScope)Parent).Continue();
            }

            _isContinue = true;
        }

        public override void Excute()
        {
            foreach (var item in Children)
            {
                if (_isBreak || _isContinue)
                    break;

                item.Excute();

            }

            Clear();
        }
    }
}
