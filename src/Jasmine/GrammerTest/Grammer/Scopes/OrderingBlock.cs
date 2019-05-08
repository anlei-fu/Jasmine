using GrammerTest.Grammer.TypeSystem;
using Jasmine.Spider.Grammer;
using System;
using System.Collections.Generic;

namespace GrammerTest.Grammer.Scopes
{
    public class OrderdedBlock : BreakableBlock
    {
        public OrderdedBlock(BreakableBlock parent) : base(parent)
        {
        }

        public IList<IExcutor> Children { get; set; } = new List<IExcutor>();
        public override void Break()
        {
            ((BreakableBlock)Parent).Break();

            _break = true;
        }

        public override void Catch(JError error)
        {
            ((BreakableBlock)Parent).Continue();

            _break = true;
        }

        public override void Continue()
        {
            ((BreakableBlock)Parent).Continue();

            _break = true;
        }
        private bool _break;

        public override void Excute()
        {
            foreach (var item in Children)
            {
                if (_break)
                    break;

                item.Excute();
            }


            _break = false;
        }

        public override void Return(JObject result)
        {
            ((BreakableBlock)Parent).Return(result);

            _break = true;
        }
    }
}
