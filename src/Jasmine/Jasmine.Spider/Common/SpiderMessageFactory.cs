using Jasmine.Spider.Worker;

namespace Jasmine.Spider.Common
{
    public class SpiderMessageFactory
    {
        public static SpiderMessage CreateDomianBlockedMessage(long taskId ,string domain,string workerId)
        {
            return null;
        }
        public static SpiderMessage CreateNewTaskMessage(long taskId)
        {
            return null;
        }
        public static SpiderMessage CreateTaskFinishedMessage(long taskId,string workerId)
        {
            return null;
        }
        public static SpiderMessage CreateTaskStatMessage(long taskId,string workerId,ISpiderTaskStat stat)
        {
            return null;
        }
        public static SpiderMessage CreateStopTaskMessage(long taskId)
        {
            return null;
        }
    }
}
