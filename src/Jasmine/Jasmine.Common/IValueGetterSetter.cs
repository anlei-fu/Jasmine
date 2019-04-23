using System;

namespace Jasmine.Common
{
    public interface IValueGetterSetter
    {
       Func<object, object> Getter { get; }
       Action<object, object> Setter { get; }
    }
}
