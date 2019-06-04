using Jasmine.Common.Attributes;
using Jasmine.Common.Exceptions;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Jasmine.Common
{
    public abstract class AbstractProcessorManager<T> : IRequestProcessorManager<T>
    {
        private ConcurrentDictionary<string, IServiceGroup> _groups = new ConcurrentDictionary<string, IServiceGroup>();
        private ConcurrentDictionary<string, IRequestProcessor<T>> _pathMap = new ConcurrentDictionary<string, IRequestProcessor<T>>();

        public abstract string Name { get; }

        public int Count => _groups.Count;

        [RestfulIgnore]
        public void AddProcessor(string path, IRequestProcessor<T> processor)
        {
           if(_pathMap.TryAdd(path,processor))
            {
                if(_groups.ContainsKey(processor.GroupName))
                {
                    _groups[processor.GroupName].AddItem(processor.Name,processor);
                }
                else
                {
                    _groups.TryAdd(processor.GroupName, new AbstractGroup());
                    _groups[processor.GroupName].AddItem(processor.Name, processor);
                }
            }
           else
            {
                throw new PathAlreadyExistsException();
            }
        }

        public IRequestProcessor<T>[] GetAllProcessor()
        {
            return _pathMap.Values.ToArray();
        }

        [RestfulIgnore]
        public bool ContainsGroup(string name)
        {
            return _groups.ContainsKey(name);
        }


        [RestfulIgnore]
        public bool ContainsProcessor(string path)
        {
            return _pathMap.ContainsKey(path);
        }
        [RestfulIgnore]
        public bool ContainsProcessor(string groupName, string name)
        {
           if(_groups.TryGetValue(groupName,out var value))
            {
                return value.GetItem(name) != null;
            }
           else
            {
                return false;
            }
        }
        [RestfulIgnore]
        public IEnumerator<IServiceGroup> GetEnumerator()
        {
            foreach (var item in _groups.Values)
            {
                yield return item;
            }
        }

        public IServiceGroup GetGroup(string name)
        {
            return _groups.TryGetValue(name, out var value) ? value : null;
        }

        [Description("获取服务统计")]
        public IRequestProcessor<T> GetProcessor(string path)
        {
            return _pathMap.TryGetValue(path, out var value) ? value : null;
        }

        public void RemoveGroup(string name)
        {
            if(_groups.TryGetValue(name,out var value))
            {
                foreach (var item in value)
                {
                    _pathMap.TryRemove(item.Path, out var _);
                }

                _groups.TryRemove(name, out var group);
            }
        }

       
        public void RemoveProcessor(string path)
        {
            if(_pathMap.TryRemove(path,out var item))
            {
                if(_groups.TryGetValue(item.GroupName,out var group))
                {
                    group.RemoveItem(item.Name);
                }
            }
        }

        public void RemoveService(string path)
        {
            if(_pathMap.TryRemove(path,out var value))
            {
               if( _groups.TryGetValue(value.GroupName,out var group))
                {
                    group.RemoveItem(value.Name);
                }
            }
        }

        public void RemoveService(string groupName, string name)
        {
            if(_groups.TryGetValue(groupName,out var group))
            {
                var item = group.GetItem(name);

                if(item!=null)
                {
                    group.RemoveItem(name);
                    _pathMap.TryRemove(item.Path, out var _);
                }
            }
        }

        public void ResumeGroup(string name)
        {
            setGroupAvailable(name, true);
        }

        public void ResumeService(string path)
        {
            setServiceAvailable(path, true);
        }

        public void ResumeService(string groupName, string serviceName)
        {
            setServiceAvailable(groupName, serviceName, true);
        }

      
     

        public void ShutDownGroup(string name)
        {
            setGroupAvailable(name, false);
        }

    

        public void ShutDownService(string path)
        {
            setServiceAvailable(path, false);
        }

        public void ShutDownService(string groupName, string serviceName)
        {
            setServiceAvailable(groupName, serviceName, false);
        }
        private void setServiceAvailable(string path, bool available)
        {
            if (_pathMap.TryGetValue(path, out var value))
            {
                value.SetAvailable(available);
            }
        }
        private void setServiceAvailable(string groupName, string serviceName, bool available)
        {
            if (_groups.TryGetValue(groupName, out var group))
            {
                var item = group.GetItem(serviceName);

                if (item is IRequestProcessor<T> processor)
                    processor.SetAvailable(available);

            }
        }
        private void setGroupAvailable(string groupName, bool available)
        {
            if (_groups.TryGetValue(groupName, out var group))
            {
                foreach (var item in group)
                {
                    if (item is IRequestProcessor<T> processor)
                        processor.SetAvailable(available);

                }
            }
        }
        [RestfulIgnore]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _groups.Values.GetEnumerator();
        }
    }
}
