using GrammerTest.Grammer.TypeSystem;
using Jasmine.Spider.Grammer;
using System;
using System.Collections.Generic;

namespace GrammerTest.Grammer.Scopes
{
    public class OrderdedBlock : BreakableBlock
    {
        public OrderdedBlock(Block parent) : base(parent)
        {
        }

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
            foreach (var item in Children)
            {
                item.Excute();
            }
        }

        public override void Return(JObject result)
        {
            throw new NotImplementedException();
        }
    }
}
