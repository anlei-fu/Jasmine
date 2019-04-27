using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;

namespace Jasmine.Spider.Grammer
{
    public  class BreakableBlock:Block
    {
        protected bool _isContinue;
        protected bool _isBreak;
        public virtual void Break()
        {
            if (Parent != null && Parent is BreakableBlock)
            {
                ((BreakableBlock)Parent).Break();
            }

            _isBreak = true;
        }
        public virtual void Continue()
        {
            if (Parent != null && Parent is BreakableBlock)
            {
                ((BreakableBlock)Parent).Continue();
            }

            _isContinue = true;
        }
      



        public virtual void Reset()
        {

        }

        public virtual void Catch(JError error)
        {
            if (Parent != null && Parent is BreakableBlock)
            {
                ((BreakableBlock)Parent).Catch(error);
            }

            _isBreak = true;
        }


        public override void Excute()
        {
            foreach (var item in Children)
            {
                if (_isBreak || _isContinue)
                    break;

                item.Excute();

            }

           Reset();
        }
    }
}
