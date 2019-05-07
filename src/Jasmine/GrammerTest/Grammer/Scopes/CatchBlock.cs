﻿using GrammerTest.Grammer.TypeSystem;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.Scopes
{
    public class CatchBlock : BodyBlock
    {
        public CatchBlock(Block parent) : base(parent)
        {
        }

        public string ErrorName { get; internal set; }
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
