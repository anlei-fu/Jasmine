﻿using GrammerTest.Grammer.TypeSystem;
using Jasmine.Spider.Grammer;
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
           Parent.Break();

            _break = true;
        }

        public override void Catch(JError error)
        {
            Parent.Catch(error);

            _break = true;
        }

        public override void Continue()
        {
            Parent.Continue();

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
            UnsetAll();
        }

        public override void Return(JObject result)
        {
            Parent.Return(result);

            _break = true;
        }
    }
}
