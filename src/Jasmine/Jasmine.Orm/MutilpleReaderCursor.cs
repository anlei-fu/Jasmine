using Jasmine.Orm.Implements;

namespace Jasmine.Orm
{
    public class MutilpleResultReaderCursor : DefaultCursor
    {
        internal MutilpleResultReaderCursor(QueryResultContext context) : base(context)
        {
        }

        public override void Close()
        {
            // do nothing
        }
    }
}
