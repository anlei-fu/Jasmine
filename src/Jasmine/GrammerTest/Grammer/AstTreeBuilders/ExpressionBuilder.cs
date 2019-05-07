﻿using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class ExpressionBuilder:BuilderBase
    {
       
     
        private static readonly string[] _interceptChars = new string[]
        {
            ";"
        };
        public ExpressionBuilder(ISequenceReader<Token> reader,bool doCheck, Block block) : base(reader, block)
        {
            _doCheck = doCheck;
        }
        private bool _doCheck;

        public override string Name => "ExpressionBuilder";

        public  Expression Build()
        {

            var expression = new Expression(_block);

            expression.Root = new AstNodeBuilder(_reader,_block, _interceptChars).Build();

            //doCheck  test expression is useful or throw
            //ex 
            //exp: a +b; this exxpression is useless

            if (_doCheck && expression.Root!=null&& !expression.Root.OperatorType.CanBeExpressionEnd())
                throwError("invalid syntax!");


            return expression;

        }

      


    }
}
