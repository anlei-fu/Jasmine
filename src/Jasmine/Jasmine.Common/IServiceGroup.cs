using System.Collections.Generic;

namespace Jasmine.Common
{
    public interface IServiceGroup:IReadOnlyCollection<IServiceItem>
    {
        IServiceItem GetItem(string name);
        bool AddItem(string name, IServiceItem item);
        void RemoveItem(string name);
        bool ContainsItem(string name);
        void Clear();

    }
}
