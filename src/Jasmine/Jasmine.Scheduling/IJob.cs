namespace Jasmine.Scheduling
{
    public   interface IJob
    {
        long Id { get; }
        bool Scheduled { get; }
        JobState JobState { get; }
        void Excute();
        void Cancel();
    }
}
