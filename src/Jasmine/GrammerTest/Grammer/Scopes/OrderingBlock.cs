using GrammerTest.Grammer.TypeSystem;
using Jasmine.Spider.Grammer;
using System;
using System.Collections.Generic;

namespace GrammerTest.Grammer.Scopes
{
    public class OrderdedBlock : BreakableBlock
    {
        public IList<IExcutor> Children { get; set; } = new List<IExcutor>();
        public override void Break()
        {
            throw new NotImplementedException();
        }

        public override void Catch(JError error)
        {
            throw new NotImplementedException();
        }

        public override void Continue()
        {
            throw new NotImplementedException();
        }

        public override void Excute()
        {
            throw new NotImplementedException();
        }

        public override void Return(JObject result)
        {
            throw new NotImplementedException();
        }
    }
}
