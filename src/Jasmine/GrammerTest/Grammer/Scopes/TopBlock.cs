﻿using GrammerTest.Grammer.TypeSystem;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.Scopes
{
    public class TopBlock : BreakableBlock
    {
        public TopBlock(Block parent) : base(parent)
        {
        }
        public TopBlock():base(null)
        {

        }

        public override void Break()
        {
            throw new System.NotImplementedException();
        }

        public override void Catch(JError error)
        {
            throw new System.NotImplementedException();
        }

        public override void Continue()
        {
            throw new System.NotImplementedException();
        }

        public override void Excute()
        {
            throw new System.NotImplementedException();
        }

        public override void Return(JObject result)
        {
            throw new System.NotImplementedException();
        }
    }
}