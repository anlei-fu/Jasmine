namespace Jasmine.Spider.Worker
{
    public interface ISpiderTaskConfigProvider
    {
        ISpiderTaskConfig Get(long id);
    }
}
