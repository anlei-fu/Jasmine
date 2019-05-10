using Jasmine.Common;
using System;

namespace Jasmine.Interpreter.TypeSystem.Factories
{
    public class JBoolFactory : AbstractSimpleQueuedPool<JBool>
    {
        public JBoolFactory(int capacity) : base(capacity)
        {
        }

        protected override JBool createNew()
        {
            throw new NotImplementedException();
        }
    }
}
