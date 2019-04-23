using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public  class ForScope:BreakableScope
    {
        public IEnumerable<object> Collection { get; set; }

        public IEnumerator<object> Enumerator { get; set; }
        public object CurrentElement { get; set; }
      
        public override void Excute()
        {
            while (Enumerator.MoveNext())
            {
                foreach (var item in Children)//excute all excutorble
                {
                    if (_isContinue)//continue has been called
                        break;

                    if (_isBreak)//break has been called
                        break;

                    item.Excute();
                }


                _isContinue = false;

                if (_isBreak)//break
                    break;

            }

            Clear();
        }

      
    }
}
