using Jasmine.Common.Attributes;
using Jasmine.Common.Exceptions;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

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
        [Description("获取全部服务数据")]
        public  IEnumerable< IServiceGroup> GetAllProcessor()
        {
            return _groups.Values;
        }

        [RestfulIgnore]
        public bool ContainsGroup([Description("服务组名")]string name)
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
   
        [Description("获取某个服务组的统计、检测、配置数据")]
        public IServiceGroup GetGroup(string name)
        {
            return _groups.TryGetValue(name, out var value) ? value : null;
        }
     
        [Description("获取某个服务的统计、检测、配置数据")]
        public IRequestProcessor<T> GetProcessor([Description("服务地址")]string path)
        {
            return _pathMap.TryGetValue(path, out var value) ? value : null;
        }
    
        [Description("移除某个服务组，注意：一旦移除，必须通过重启服务器才能恢复服务")]
        public void RemoveGroup([Description("服务组名")]string name)
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

       [RestfulIgnore]
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
        [Description("移除某个服务，注意：一旦移除，必须通过重启服务器才能恢复服务")]
        public void RemoveService([Description("服务地址")]string path)
        {
            if(_pathMap.TryRemove(path,out var value))
            {
               if( _groups.TryGetValue(value.GroupName,out var group))
                {
                    group.RemoveItem(value.Name);
                }
            }
        }
        [RestfulIgnore]
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
        [Description("恢复某个服务组，使之正常运行")]
        public void ResumeGroup([Description("服务组名")]string name)
        {
            setGroupAvailable(name, true);
        }
        [Description("重启某个服务")]
        public void ResumeService([Description("服务地址")]string path)
        {
            setServiceAvailable(path, true);
        }
        [RestfulIgnore]
        public void ResumeService(string groupName, string serviceName)
        {
            setServiceAvailable(groupName, serviceName, true);
        }

      
        [Description("关闭某个服务组")]
        public void ShutDownGroup([Description("服务组名")]string name)
        {
            setGroupAvailable(name, false);
        }

        [Description("关闭某个服务")]
        public void ShutDownService([Description("服务路径")]string path)
        {
            setServiceAvailable(path, false);
        }
        [RestfulIgnore]
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
