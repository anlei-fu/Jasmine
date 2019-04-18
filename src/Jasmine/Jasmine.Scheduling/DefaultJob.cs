using System;

namespace Jasmine.Scheduling
{
    public class DefaultJob 
    {


        private static int _id = 0;

        private Action<object> _excuter;
        public long Id => throw new NotImplementedException();

        public JobState JobState { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public object State => throw new NotImplementedException();

        public void Excute(object state)
        {
            throw new NotImplementedException();
        }
    }
}
