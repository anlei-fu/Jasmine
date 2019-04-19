namespace Jasmine.Common
{
    public class StatItemBase : IStatItem
    {
        public  StatItemBase(int time,bool suceed)
        {
            Time = time;
            Sucessed = suceed;
        }
        public int Time { get; }

        public bool Sucessed { get; }
    }
}
