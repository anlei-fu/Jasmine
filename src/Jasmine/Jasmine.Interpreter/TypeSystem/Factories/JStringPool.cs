using Jasmine.Common;

namespace Jasmine.Interpreter.TypeSystem.Factories
{
    public class JStringPool : AbstractSimpleQueuedPool<JString>
    {
        public JStringPool(int capacity) : base(capacity)
        {
        }

        protected override JString newInstance()
        {
            throw new System.NotImplementedException();
        }
    }
}
