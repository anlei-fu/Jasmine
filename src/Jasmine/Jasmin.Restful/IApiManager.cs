namespace Jasmine.Restful
{
    public interface IApiManager
    {
        void ShutDownServiceGroup(string path);
        void ShutDownService(string path);
        void ResumeServiceGroup(string path);
        void ResumeService(string path);
    }
}
