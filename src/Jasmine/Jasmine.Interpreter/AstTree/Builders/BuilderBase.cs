using Jasmine.Interpreter.AstTree.Builders.Exceptions;
using Jasmine.Interpreter.Scopes;
using Jasmine.Interpreter.Tokenizers;
using System;
using System.Runtime.CompilerServices;

namespace Jasmine.Interpreter.AstTree.Builders
{
    public abstract class BuilderBase
    {
        
        public  BuilderBase(ISequenceReader<Token> reader,BreakableBlock block)
        {
            _reader = reader;
            _block = block;
        }
        protected BreakableBlock _block;
        protected ISequenceReader<Token> _reader;
        protected OperatorNode _currentNode;
        /// <summary>
        /// mark current builder type
        /// </summary>
        public abstract string Name { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void throwUnexceptedError()
        {
            throwError($"unexcepted '{_reader.Current().Value}';");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void throwError(string msg)
        {
            throw new BuilderException(msg, Name, _reader.Current().Line, _reader.Current().LineNumber);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void throwErrorIfHasNoNextOrNext(string msg)
        {
            if (!_reader.HasNext())
                throwError(msg);

            _reader.Next();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void throwErrorIfOperatorTypeNotMatch(OperatorType type)
        {

            if (_reader.Current().OperatorType != type)
                throwUnexceptedError();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void throwIf(Func<Token,bool> predict)
        {
            if (predict(_reader.Current()))
                throwUnexceptedError();
        }
    }
}
