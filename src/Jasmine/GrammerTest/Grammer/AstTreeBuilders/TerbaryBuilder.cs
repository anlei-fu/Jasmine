using GrammerTest.Grammer.AstTree;
using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class TerbaryBuilder : BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            "?",":",";"
        };
        public TerbaryBuilder(ISequenceReader<Token> reader, BreakableBlock block ):base(reader,block)
        {
        }

        public override string Name => "TernaryBuilder";


        public  TernaryOperatorNode Build()
        {
            TernaryOperatorNode ternaryNode = new TernaryOperatorNode();


            while (_reader.HasNext())
            {
                var node = new AstNodeBuilder(_reader,_block,_interceptChars).Build();

                if (node == null)
                    throwError("ternary empty expression is not allowed!");

                /*
                 * expression end
                 * 
                 */
                if (_reader.Current().Value == ";")
                {
                    if (ternaryNode.Operands.Count == 2)//more two 
                    {
                        throwError("ternary expression incorrect!");
                    }
                    else if (ternaryNode.Operands.Count == 0)// only  one
                    {
                        throwError("ternary expression incorrect!");
                    }
                    else
                    {
                        ternaryNode.Operands.Add(node);

                        return ternaryNode;
                    }
                }
                /*
                 * start a new ternary builder
                 * 
                 */
                else if (_reader.Current().Value == "?")
                {
                    if (!node.OutputType.IsBool())
                        throwError("before '?' require a expression which output is bool! ");

                    var ternaryNode2 = new TerbaryBuilder(_reader,_block).Build();
                    ternaryNode2.Operands.Insert(0, node);

                    ternaryNode.Operands.Add(ternaryNode2);



                    if (_reader.Current().Value == ";")
                    {
                        if (ternaryNode.Operands.Count != 2)
                            throwError("incompleted ternary exception!");

                        return ternaryNode;
                    }

                }
                else if (_reader.Current().Value == ":")
                {
                    if (ternaryNode.Operands.Count == 1)
                    {
                        ternaryNode.Operands.Add(node);

                        return ternaryNode;
                    }
                    else
                    {
                        ternaryNode.Operands.Add(node);
                    }
                }
            }


            throwError("incompleted ternary expression!");

            return ternaryNode;

        }

    }
}
