using Jasmine.Parsers.Html;
using System;
using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public  class AstTree
    {
        private Element _element;
        public bool Match(Element element)
        {
            return true;
        }

        public class StringParameter
        {
            public string Pattern { get; set; }
            public bool IsLike { get; set; }
        }

        public  class OperandNode
        {
            public OperatorNode ParentOperator { get; set; }
            public OperatorNode ChildOperator { get; set; }
            public bool BoolResult { get; set; }
            public string StringResult { get; set; }
            
        }
        public  class ParameterOperandNode
        {
           public IList<StringParameter> Parameters { get; set; }
        }

        public abstract class OperatorNode
        {
          
            public string Name { get; set; }
            public OperandNode Left { get; set; }
            public OperandNode Right { get; set; }
            public OperandNode Output { get; set; }


            public void Operate()
            {

            }
            public abstract void DoOperate(Element element);

           
        }

       

        public class GetNameOperator : OperatorNode
        {
            public override void DoOperate(Element element)
            {
                if (Output == null)
                    throw new AstGrammerException("getname function output node is null");

                if (Right == null)
                    throw new AstGrammerException("getname function right parameter node is null");

                if (Right.StringResult == null)
                    throw new AstGrammerException($"getname function , right operand node string result is null");

                Output.StringResult = element.Name;

            }
        }

        public class GetAttributeValueOperator : OperatorNode
        {
            public override void DoOperate(Element element)
            {
                if (Output == null)
                    throw new AstGrammerException("getname function output node is null");

                if (Right == null)
                    throw new AstGrammerException("getname function right parameter node is null");

                if (Right.StringResult == null)
                    throw new AstGrammerException($"getname function , right operand node string result is null");

                Output.StringResult = element.Attributes.GetAttribute(Right.StringResult)?.Value;

            }
        }

        public class ContainsAttributeOperator : OperatorNode
        {
            public override void DoOperate(Element element)
            {
                if (Output == null)
                    throw new AstGrammerException("getname function output node is null");

                if (Right == null)
                    throw new AstGrammerException("getname function right parameter node is null");

                if (Right.StringResult == null)
                    throw new AstGrammerException($"getname function , right operand node string result is null");

                Output.BoolResult = element.Attributes.Contains(Right.StringResult);
            }
        }
        public class ContainsAnyAttributeOperator : OperatorNode
        {
            public override void DoOperate(Element element)
            {
                throw new NotImplementedException();
            }
        }
        public class ContainsAllOperator : OperatorNode
        {
            public override void DoOperate(Element element)
            {
                throw new NotImplementedException();
            }
        }

        public class EquelOperator : OperatorNode
        {
            public override void DoOperate(Element element)
            {
                throw new NotImplementedException();
            }
        }
        public class NotEquelOperator : OperatorNode
        {
            public override void DoOperate(Element element)
            {
                throw new NotImplementedException();
            }
        }

        public class NotOperator : OperatorNode
        {
            public override void DoOperate(Element element)
            {
                throw new NotImplementedException();
            }
        }
        public class AndOperator : OperatorNode
        {
            public override void DoOperate(Element element)
            {
                throw new NotImplementedException();
            }
        }
        public class OrOperator : OperatorNode
        {
            public override void DoOperate(Element element)
            {
                throw new NotImplementedException();
            }
        }

     
    }
}
