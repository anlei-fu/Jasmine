﻿using System;

namespace Jasmine.Interpreter.Tokenizers.Exceptions
{
    public  class TokenizerExcption:Exception
    {
        public TokenizerExcption(int line,int lineNumber,string msg):base($"{msg},At line {line} and lineNumber {lineNumber}")
        {
        }
    }
}
