using Jasmine.Common;
using System;

namespace Jasmine.Interpreter.Scopes
{
    public class ExcutingStackPool : AbstractSimpleQueuedPool<ExcutingStack>,IDisposable
    {
        ExcutingStackPool(int capacity) : base(capacity)
        {
            start();
        }

        public static readonly ExcutingStackPool Instance = new ExcutingStackPool(10000);

        protected override ExcutingStack newInstance()
        {
            return new ExcutingStack();
        }

        public override void Recycle(ExcutingStack item)
        {
            base.Recycle(item);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private void start()
        {

        }
    }
}
