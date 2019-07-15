namespace Jasmine.Reflection
{
    public  interface INameMapper<T>
    {
        string[] GetAllNames();
        T GetItemByName(string name);
        bool Contains(string name);
    }
}
