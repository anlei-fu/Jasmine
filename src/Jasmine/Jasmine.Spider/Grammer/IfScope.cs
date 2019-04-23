namespace Jasmine.Spider.Grammer
{
    public class IfScope: BreakableScope
    {
       private bool _isMatchFound;

        public void SetMatchFound()
        {
            _isMatchFound = true;
        }

        public override void Excute()
        {
            foreach (var item in Children)
            {
                if (_isMatchFound)
                    break;

                item.Excute();
            }

        }


    }
}
