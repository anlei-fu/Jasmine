using Jasmine.Spider.Grammer;
using System.Collections.Generic;

namespace GrammerTest.Grammer.Scopes
{
    public abstract class Block : AbstractExcutor, IVariableTable
    {
        private Dictionary<string, JObject> _variables = new Dictionary<string, JObject>();
        public Block(Block parent) : base(parent)
        {
        }

        public override string Name => ".Block";

        public IVariableTable Parnet { get; internal set; }
        public JObject Declare(string name)
        {
            if (_variables.ContainsKey(name))
                throw new System.Exception();
            else
            {
                _variables.Add(name, new JObject());
            }

            return _variables[name];
        }

        public JObject Declare(string name, JObject obj)
        {
            if(_variables.ContainsKey(name))
                throw new System.Exception();
            else
            {
                _variables.Add(name, obj);
            }

            return _variables[name];
        }

        public JObject GetVariable(string name)
        {
            if (_variables.ContainsKey(name))
                return _variables[name];
            else
            {
                if (Parent == null)
                    throw new System.Exception();

                return Parent.GetVariable(name);
            }
        }

        public void Reset(string name, JObject obj)
        {
            _variables[name] = obj;
        }

        public void Unset(string name)
        {
            throw new System.NotImplementedException();
        }

        public void UnsetAll(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
