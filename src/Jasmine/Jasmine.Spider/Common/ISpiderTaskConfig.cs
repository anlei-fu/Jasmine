using System.Collections.Generic;

namespace Jasmine.Spider.Common
{
    public interface ISpiderTaskConfig:INameFearture
    {
        long TaskId { get; }
        string Owner { get; }
        int MaxSpeed { get; }
        bool AllowMutiple { get; }
        bool UseProxy { get; }
        IList<string> Matcher { get; }
        IDictionary<string, string[]> ErrorMatcher { get; }
        string Description { get; }
        IList<string> StartPages { get; }
        IList<CookieItem> Cookies { get; }
        bool UseCookie { get; }
      
    }
}
