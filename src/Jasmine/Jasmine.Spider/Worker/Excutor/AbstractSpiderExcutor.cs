using Jasmine.Spider.Interface;
using Jasmine.Spider.Worker.Excutor.Downloader;
using System;

namespace Jasmine.Spider.Worker.Excutor
{
    public class AbstractSpiderTaskExcutor : ISpiderTaskExcutor
    {
        private ISpiderTaskConfig _config;
        private ISpiderTaskStat _stat;
        private IDownloader _downloader;
        private IExtractor _extractor;
        
        public string Id => throw new NotImplementedException();

        public ISpiderTaskStat Stat => throw new NotImplementedException();

        public void Excute()
        {
            throw new NotImplementedException();
        }

        public void ReConfig(ISpiderTaskConfig config)
        {
            _config = config;
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
