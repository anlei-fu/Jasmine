using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Jasmine.Interpreter.TypeSystem.Factories
{
    public  class AnyPool:IDisposable
    {
        private AnyPool()
        {
            Task.Factory.StartNew(workLoop, TaskCreationOptions.LongRunning);
        }

        public static readonly AnyPool Instance = new AnyPool();
        public void Recycle(Any any)
        {
            _queue.Enqueue(any);
            Monitor.PulseAll(_lock);
        }

        

        private JStringPool _stringPool = new JStringPool(10000);
        private JNumberPool _numberPool = new JNumberPool(10000);
        private JBoolPool _boolPool = new JBoolPool(10000);
        private JObjectPool _objectPool = new JObjectPool(10000);
        private ConcurrentQueue<Any> _queue = new ConcurrentQueue<Any>();

        public JBool GetJBool()
        {
            return _boolPool.Rent();
        }
        public JString GetJstring(string str)
        {
            var item = _stringPool.Rent();
            item.Value = str;

            return item;
        }
        public JNumber GetJnumber()
        {
            return _numberPool.Rent();
        }
        public JObject GetJobject()
        {
            return _objectPool.Rent();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private readonly object _lock = new object();
        private void workLoop()
        {
            while (true)
            {
                if(_queue.TryDequeue(out var value))
                {
                    if(value is JString js)
                    {
                        _stringPool.Recycle(js);
                    }
                    else if(value is JBool jb)
                    {
                        _boolPool.Recycle(jb);
                    }
                    else if(value is JNumber jn)
                    {
                        _numberPool.Recycle(jn);
                    }
                    else if(value is JObject jo)
                    {
                        _objectPool.Recycle(jo);
                    }
                }
                else
                {
                    Monitor.Wait(_lock, 100);
                }
            }
        }
    }
}
