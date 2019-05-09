using GrammerTest.Grammer.Scopes.Exceptions;
using GrammerTest.Grammer.TypeSystem.Exceptions;
using Jasmine.Spider.Grammer;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GrammerTest.Grammer.Scopes
{
    public abstract class Block : AbstractExcutor, IVariableTable
    {
        private Dictionary<string, JObject> _variables = new Dictionary<string, JObject>();
        public Block(BreakableBlock parent) : base(parent)
        {
        }

        public override string Name => ".Block";

        public IVariableTable Parnet { get; internal set; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public JObject Declare(string name)
        {
            if (_variables.ContainsKey(name))
                throw new VaribleAlreadyDeclaredException();
            else
            {
                _variables.Add(name, new JUndefined(name));
            }

            return _variables[name];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public JObject Declare(string name, JObject obj)
        {
            if(_variables.ContainsKey(name))
                throw new System.Exception();
            else
            {
                obj.Name = name;
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
            

            if (_variables.ContainsKey(name))
            {
                obj.Name = name;
                _variables[name] = obj;
            }
            else
            {
                if (Parent != null)
                    Parent.Reset(name, obj);
                else
                    throw new VariableNotFoundException();


            }

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Unset(string name)
        {
            if (_variables.ContainsKey(name))
                _variables.Remove(name);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UnsetAll()
        {
            _variables.Clear();
        }

      
    }
}
