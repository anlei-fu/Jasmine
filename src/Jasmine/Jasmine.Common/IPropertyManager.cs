using System.Collections.Generic;

namespace Jasmine.Common
{
    public interface  IPropertyManager:IReadOnlyCollection<KeyValuePair<string,string>>
    {
        string GetValue(string key);
        void SetValue(string key, string value);
    }
}
