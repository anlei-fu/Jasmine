using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public  class OperatorNodeFactory
    {

    

        public static AndOperatorNode CreateAnd()
        {
            return new AndOperatorNode();
        }
        public static OrOperatorNode CreateOr()
        {
            return null;
        }

        public static NotOperatorNode CreateNot()
        {
            return null;
        }

        public static AddOperatornNode CreateAdd()
        {
            return null;
        }

        public static ReduceOperatorNode CreateReduce()
        {
            return null;
        }

        public static MutiplyOperatorNode CreateMutiply()
        {
            return null;
        }

        public static DevideOperatorNode CreateDevide()
        {
            return null;
        }

        public static AssignmentOperatorNode CreateAssigment()
        {
            return null;
        }

        public static ModOperatorNode CreateMod()
        {
            return null;
        }

        public static  BiggerOperatorNode CreateBigger()
        {
            return null;
        }

        public static BiggerEquelOperatorNode CreateBiggerEquelNode()
        {
            return null;
        }

        public static LessOperatorNode CreateLess()
        {
            return null;
        }

        public static LessEquelOperatorNode CreateLessEquel()
        {
            return null;
        }



        public static AstNode CreateStringNode(AstNode type)
        {
            return null;
        }

        public static OperandNode CreateOperrand(JObject obj)
        {
            return new OperandNode(obj);
        }

        public static AstNode CreateNumberNode(string value)
        {
            return null;
        }

    
        public static CompareEquelOperatorNode CreateCompareEquel()
        {
            return null;
        }

        public static ComareNotEquelNode CreateCompareNotEquel()
        {
            return null;
        }

        public static MemberOperaterNode CreateMemeber()
        {
            return null;
        }

        public static IncrementNode  CreateIncrement()
        {
            return null;
        }

        public static DcrementNode CreateDecremnet()
        {
            return null;
        }

        public static NewOperatorNode CreateNew()
        {
            return null;
        }

        
    }
}
