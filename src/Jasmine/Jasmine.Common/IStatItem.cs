using System;

namespace Jasmine.Common
{
    public interface IStatItem
    {
        long Elapsed { get; }
        bool Sucessed { get; }
        string Time { get; }
    }
}
