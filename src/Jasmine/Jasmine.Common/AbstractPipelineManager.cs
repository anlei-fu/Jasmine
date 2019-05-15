using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class AbstractPipelineManager<TContext> : IRequestProcessorManager<TContext>
    {

        private IGroupManager _manager;
        public int Count => throw new System.NotImplementedException();

        public string Name => throw new System.NotImplementedException();



        public void AddProcessor(string path, IRequestProcessor<TContext> processor)
        {
            
        }

        public bool ContainsGroup(string name)
        {
            return _manager.ContainsGroup(name);
        }

        public bool ContainsProcessor(string path)
        {
            throw new System.NotImplementedException();
        }

        public bool ContainsProcessor(string groupName, string name)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<IRequestProcessor<TContext>> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public IRequestProcessor<TContext> GetProcessor(string path)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveGroup(string name)
        {
            _manager.RemoveGroup(name);
        }

        public void RemoveProcessor(string path)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveService(string path)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveService(string groupName, string name)
        {
            throw new System.NotImplementedException();
        }

        public void ResumeGroup(string name)
        {
            throw new System.NotImplementedException();
        }

        public void ResumeService(string path)
        {
            throw new System.NotImplementedException();
        }

        public void ResumeService(string groupName, string serviceName)
        {
            throw new System.NotImplementedException();
        }

        public void ShutDownGroup(string name)
        {
            throw new System.NotImplementedException();
        }

        public void ShutDownService(string path)
        {
            throw new System.NotImplementedException();
        }

        public void ShutDownService(string groupName, string serviceName)
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
