namespace Jasmine.Reflection.Interfaces
{
    public  interface INameFearture<T>
    {
        string[] GetAllNames();
        T GetItemByName(string name);
        bool Contains(string name);
    }
}
