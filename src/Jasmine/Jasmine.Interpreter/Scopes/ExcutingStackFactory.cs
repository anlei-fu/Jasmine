using Jasmine.Common;

namespace Jasmine.Interpreter.Scopes
{
    public class ExcutingStackFactory : AbstractSimpleQueuedPool<ExcutingStack>
    {
        public ExcutingStackFactory(int capacity) : base(capacity)
        {
        }

        protected override ExcutingStack newInstance()
        {
            throw new System.NotImplementedException();
        }
        public override void Recycle(ExcutingStack item)
        {
            base.Recycle(item);
        }
    }
}
