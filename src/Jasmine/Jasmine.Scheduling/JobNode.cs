namespace Jasmine.Scheduling
{
    public class JobNode
    {
        JobNode Next { get; set; }
        JobNode Previous { get; set; }
        public ITimeoutJob Job { get; set; }
    }

    public class JobList
    {
        private readonly JobNode _head;
        private readonly JobNode _tail;

        public int Count { get; private set; }
        public void Add(JobNode node)
        {

        }
        public void Remove(JobNode node)
        {

        }
        public void ReAdd(JobNode node)
        {

        }
        public void Clear()
        {

        }
        public  int Capacity { get; private set; }


    }
}
