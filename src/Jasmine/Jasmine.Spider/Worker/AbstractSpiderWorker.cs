using Jasmine.Spider.Common;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jasmine.Spider.Worker
{
    public class AbstractSpiderWorker : ISpiderWorker
    {

        private IWorkerMessager _messager;
        private ISpiderTaskConfigProvider _configProvider;
        private ISpiderRpcClient _rpcClient;
  
        private ConcurrentDictionary<long, LinkedList<ISpiderTaskExcutor>> _excutors = new ConcurrentDictionary<long, LinkedList<ISpiderTaskExcutor>>();
        private IExcutorFactary _excutorFactory;
        public string Id => throw new NotImplementedException();

        public IEnumerator<ISpiderTask> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void ReportDomainBlocked(long taskId,string domain)
        {
            _messager.SendMessage(SpiderMessageFactory.CreateDomianBlockedMessage(taskId, domain, Id));
        }

        public void ReportTaskFinished(long taskId,string workerId)
        {
            _messager.SendMessage(SpiderMessageFactory.CreateTaskFinishedMessage(taskId, workerId));
        }

        public Task StartAsync()
        {
            throw new NotImplementedException();
        }

        public void StartTask(long id)
        {
            var config = _rpcClient.GetConfig(id);

            var excutor = _excutorFactory.Create();

            if (!_excutors.ContainsKey(id))
                _excutors.TryAdd(id, new LinkedList<ISpiderTaskExcutor>());


            _excutors[id].AddLast(excutor);

            excutor.Excute(config);

        }

        public Task StopAsync()
        {
            throw new NotImplementedException();
        }

        public void StopTask(long id)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
