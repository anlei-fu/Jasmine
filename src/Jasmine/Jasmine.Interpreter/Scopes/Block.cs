using GrammerTest.Grammer.TypeSystem.Exceptions;
using Jasmine.Interpreter.Scopes.Exceptions;
using Jasmine.Interpreter.TypeSystem;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Jasmine.Interpreter.Scopes
{
    public abstract class Block : AbstractExcutor, IVariableTable
    {
        private Dictionary<string, Any> _variables = new Dictionary<string, Any>();
        public Block(BreakableBlock parent) : base(parent)
        {
        }

        public override string Name => ".Block";

        public IVariableTable Parnet { get; internal set; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Any Declare(string name)
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
        public Any Declare(string name, Any obj)
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


        public Any GetVariable(string name)
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

        public void Reset(string name, Any obj)
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
