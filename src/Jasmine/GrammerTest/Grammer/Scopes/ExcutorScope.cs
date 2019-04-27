using Jasmine.Spider.Grammer;
using System;
using System.Collections.Generic;

namespace GrammerTest.Grammer
{
    public class ExcutorScope : Scope
    {
        private Dictionary<string, Type> _typeMapping = new Dictionary<string, Type>();

        public void ImportNewType(string name,Type type)
        {

        }

        public override void Excute()
        {
            throw new NotImplementedException();
        }
    }
}
