using System.Collections.Generic;

namespace Jasmine.Common
{
    public interface IGroup:IReadOnlyCollection<IGroupItem>
    {
        IGroupItem GetItem(string name);
        bool AddItem(string name, IGroupItem item);
        void RemoveItem(string name);
        bool ContainsItem(string name);
        void Clear();

    }
}
