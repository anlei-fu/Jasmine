using System.Collections.Generic;

namespace Jasmine.Common
{
    public interface  IPropertyManager
    {
        string GetValue(string key);
        void SetValue(string key, string value);
    }
}
