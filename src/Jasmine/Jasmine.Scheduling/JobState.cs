namespace Jasmine.Scheduling
{
    public enum JobState
    {
        /// <summary>
        /// 尚未被计划
        /// </summary>
        UnScheduled,
        /// <summary>
        /// 已计划
        /// </summary>
        Scheduled,
        /// <summary>
        /// 正在执行
        /// </summary>
        Excuting,
        /// <summary>
        /// 成功完成
        /// </summary>
        CompleteSuccessfully,
        /// <summary>
        /// 执行完成且失败
        /// </summary>
        CompleteFailed,
    }
}
