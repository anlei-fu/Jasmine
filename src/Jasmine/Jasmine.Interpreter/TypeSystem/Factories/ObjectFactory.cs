using Jasmine.Common;
using System;

namespace Jasmine.Interpreter.TypeSystem.Factories
{
    public class JObjectPool:AbstractSimpleQueuedPool<JObject>
    {
        public JObjectPool(int capacity) : base(capacity)
        {
        }

        protected override JObject createNew()
        {
            throw new NotImplementedException();
        }
    }
}
