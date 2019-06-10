using System;

namespace Jasmine.Scheduling.Exceptions
{
    public class JobExcuteException:Exception
    {
        public JobExcuteException(long jobId,  Exception ex):base($"job({jobId}) excute faild !",ex)
        {

        }
    }
}
