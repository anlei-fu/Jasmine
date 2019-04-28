﻿using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;
using Jasmine.Spider.Grammer;
using System;

namespace GrammerTest.Grammer
{
    public class TryCatchFinallyBlock : BreakableBlock
    {
        public TryBlock TryBlock { get; set; }
        public CatchBlock CatchBlock { get; set; }
        public TryCatchFinallyBlock FinallyBlock { get; set; }

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
