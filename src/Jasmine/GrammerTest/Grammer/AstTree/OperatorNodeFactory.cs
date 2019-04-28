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
            return new OrOperatorNode();
        }

        public static NotOperatorNode CreateNot()
        {
            return new NotOperatorNode();
        }

        public static AddOperatorNode CreateAdd()
        {
            return  new AddOperatorNode();
        }

        public static ReduceOperatorNode CreateReduce()
        {
            return new ReduceOperatorNode();
        }

        public static MutiplyOperatorNode CreateMutiply()
        {
            return new MutiplyOperatorNode();
        }

        public static DevideOperatorNode CreateDevide()
        {
            return new DevideOperatorNode();
        }

        public static AssignmentOperatorNode CreateAssigment()
        {
            return new AssignmentOperatorNode();
        }

        public static ModOperatorNode CreateMod()
        {
            return  new ModOperatorNode();
        }

        public static  BiggerOperatorNode CreateBigger()
        {
            return new BiggerOperatorNode();
        }

        public static BiggerEquelOperatorNode CreateBiggerEquelNode()
        {
            return new BiggerEquelOperatorNode();
        }

        public static LessOperatorNode CreateLess()
        {
            return new LessOperatorNode();
        }

        public static LessEquelOperatorNode CreateLessEquel()
        {
            return new LessEquelOperatorNode();
        }



        public static AstNode CreateStringNode(AstNode type)
        {
            return null;
        }

        public static OperandNode CreateOperrand(JObject obj)
        {
            return new OperandNode(obj);
        }

        public static OperandNode CreateNumberNode(string value)
        {
            return null;
        }

    
        public static CompareEquelOperatorNode CreateCompareEquel()
        {
            return  new CompareEquelOperatorNode();
        }

        public static ComareNotEquelNode CreateCompareNotEquel()
        {
            return new ComareNotEquelNode();
        }

        public static MemberOperaterNode CreateMemeber()
        {
            return new MemberOperaterNode();
        }

        public static IncrementNode  CreateIncrement()
        {
            return new IncrementNode();
        }

        public static DcrementNode CreateDecremnet()
        {
            return new DcrementNode();
        }

        public static NewOperatorNode CreateNew()
        {
            return new NewOperatorNode();
        }

        
    }
}
