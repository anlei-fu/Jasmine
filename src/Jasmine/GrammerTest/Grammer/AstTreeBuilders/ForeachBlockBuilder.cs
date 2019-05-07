﻿using GrammerTest.Grammer.AstTreeBuilders;
using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class ForeachBuilder : BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            ")"
        };

        public ForeachBuilder(ISequenceReader<Token> reader, Block block) : base(reader, block)
        {
        }

        public override string Name => "ForeachBuilder";

        public ForeachBlock Build()
        {
            var foreachBlock = new ForeachBlock(_block);

            /*
             *  resolve header
             * 
             */
             
            throwErrorIfHasNoNextAndNext("incompleted foreach block;");

            throwErrorIfOperatorTypeNotMatch(OperatorType.LeftParenthesis);//(

            throwErrorIfHasNoNextAndNext("incompleted foreach block;");

            throwErrorIfOperatorTypeNotMatch(OperatorType.Declare);//(var

            throwErrorIfHasNoNextAndNext("incompleted foreach block;");

            throwIf(x =>!x.IsIdentifier());//(var item

            var name = _reader.Current().Value;

            throwErrorIfHasNoNextAndNext("incompleted foreach block;");

            throwIf(x => x.Value != Keywords.IN);//(var item in

            foreachBlock.DeclareExpression = new DeclareExpression(_block)
            {
                Root = new DeclareOperator(_block)
            };

            foreachBlock.DeclareExpression.Root.Operands.Add(new OperandNode(new JString(name)));//(var item in 

            foreachBlock.GetCollectionExpression = new Expression(_block);

            foreachBlock.GetCollectionExpression.Root = new AstNodeBuilder(_reader,_block ,_interceptChars).Build();

            /*
             * Resolve body
             * 
             */

            if (!_reader.HasNext())
                throwErrorIfHasNoNextAndNext("incompleted foreach block;");

            foreachBlock.Body = new OrderedBlockBuilder(_reader,"foreach",foreachBlock).Build();

            return foreachBlock;

        }
    }
}
