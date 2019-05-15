using System;
using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class AbstractProcessorManager<T> : IRequestProcessorManager<T>
    {

        public string Name => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

      

        public void AddProcessor(string path, IRequestProcessor<T> processor)
        {
           
        }

      

        public bool ContainsGroup(string name)
        {
            throw new NotImplementedException();
        }

     

        public bool ContainsProcessor(string path)
        {
            throw new NotImplementedException();
        }

        public bool ContainsProcessor(string groupName, string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IGroup> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IGroup GetGroup(string name)
        {
            throw new NotImplementedException();
        }

        public IRequestProcessor<T> GetProcessor(string path)
        {
            throw new NotImplementedException();
        }

        public void RemoveGroup(string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveProcessor(string path)
        {
            throw new NotImplementedException();
        }

        public void RemoveService(string path)
        {
            throw new NotImplementedException();
        }

        public void RemoveService(string groupName, string name)
        {
            throw new NotImplementedException();
        }

        public void ResumeGroup(string name)
        {
            throw new NotImplementedException();
        }

        public void ResumeService(string path)
        {
            throw new NotImplementedException();
        }

        public void ResumeService(string groupName, string serviceName)
        {
            throw new NotImplementedException();
        }

        public void ShutDownGroup(string name)
        {
            throw new NotImplementedException();
        }

        public void ShutDownService(string path)
        {
            throw new NotImplementedException();
        }

        public void ShutDownService(string groupName, string serviceName)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
