using System.Collections.Generic;

namespace Jasmine.Common
{
    public   interface IGroupManager:IReadOnlyCollection<IGroup>
    {
        IGroup GetGroup(string name);
        bool AddGroup(string name, IGroup group);
        void RemoveGroup(string name);
        bool ContainsGroup(string name);
        void Clear();

    }
}
