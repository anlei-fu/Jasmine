using System.Collections.Generic;

namespace Jasmine.Scheduling
{
    public   class JasmineFifoScheduler<T>
    {
        private Queue<T> _taskQueue;
        public void Schedule(T task)
        {
            _taskQueue.Enqueue(task);
        }

        public T GetTask()
        {
            return _taskQueue.Dequeue();
        }
    }
}
