using GrammerTest.Grammer.Exceptions;
using System;

namespace GrammerTest.Grammer.Scopes
{
    public class PropertyNotDeclareException : ExcutingException
    {
        public PropertyNotDeclareException(string msg) : base(msg)
        {
        }
    }
}
