using Jasmine.Interpreter.TypeSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Interpreter.Scopes
{
  public  class ExcutingStackFrame
    {
        public Stack<Any> Stack { get; set; }
        public Dictionary<string,Any> Locals { get; set; }
        public Dictionary<string, ExcutingStackFrame> TempLocals { get; set; }
        public List<ExcutingStackFrame> Children { get; set; }


    }
}
