using Jasmine.Interpreter.Scopes;
using Jasmine.Interpreter.Tokenizers;

namespace Jasmine.Interpreter.AstTree.Builders
{
    public class TryCatchFinallyBlockBuilder : BuilderBase
    {
        public TryCatchFinallyBlockBuilder(ISequenceReader<Token> reader, BreakableBlock block) : base(reader, block)
        {
        }

        public override string Name => "TryCatchFinallyBuilder";
        public TryCatchFinallyBlock Build()
        {
            var tryCatchFinallyBlock = new TryCatchFinallyBlock(_block);

            tryCatchFinallyBlock.TryBlock = new TryBlockBuilder(_reader,_block).Build();
            /*
             * build catch block or finally block
             * 
             */ 
            if (!_reader.HasNext())
                return tryCatchFinallyBlock;

            _reader.Next();

            if (_reader.Current().Value == Keywords.FINALLY)
            {
                tryCatchFinallyBlock.FinallyBlock = new FinalyBlockBuilder(_reader,_block).Build();

                return tryCatchFinallyBlock;
            }
            else if (_reader.Current().Value == Keywords.CATCH)
            {
                tryCatchFinallyBlock.CatchBlock = new CatchBuilder(_reader,_block).Build();
            }
            else
            {
                _reader.Back();

                return tryCatchFinallyBlock;
            }

            /*
             * build finally block
             * 
             */ 
            if (!_reader.HasNext())
                return tryCatchFinallyBlock;

            _reader.Next();

            if (_reader.Current().Value == Keywords.FINALLY)
            {
                /*
                 * finally block has been set;
                 */ 
                if (tryCatchFinallyBlock.FinallyBlock != null)
                    throwError("incorrect try-catch-finally block!");

                tryCatchFinallyBlock.FinallyBlock = new FinalyBlockBuilder(_reader,_block).Build();
            }
            else if (_reader.Current().Value == Keywords.CATCH)
            {
                /*
                 * can not exist mutiple catch block  or finally block before catch block
                 */ 
                throwError("incorrect try-catch-finally block!");
            }
            else
            {
                _reader.Back();

                return tryCatchFinallyBlock;
            }

            return tryCatchFinallyBlock;

        }
    }
}
