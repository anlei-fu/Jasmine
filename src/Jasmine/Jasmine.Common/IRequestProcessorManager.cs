using System.Collections.Generic;

namespace Jasmine.Common
{
    public  interface IRequestProcessorManager<T>:IReadOnlyCollection<IRequestProcessor<T>>,INameFearture
    {
        void AddProcessor(string path, IRequestProcessor<T> processor);
        void RemoveProcessor(string path);
        bool ContainsProcessor(string path);
        bool ContainsProcessor(string groupName, string name);
        bool ContainsGroup(string name);
        IRequestProcessor<T> GetProcessor(string path);
        void ShutDownGroup(string name);
        void ShutDownService(string path);
        void ShutDownService(string groupName, string serviceName);
        void ResumeGroup(string name);
        void ResumeService(string path);
        void ResumeService(string groupName, string serviceName);
        void RemoveGroup(string name);
        void RemoveService(string path);
        void RemoveService(string groupName, string name);


    }
}
