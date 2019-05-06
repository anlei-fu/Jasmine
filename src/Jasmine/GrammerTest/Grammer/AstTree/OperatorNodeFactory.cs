using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public  class OperatorNodeFactory
    {

    

        public static AndOperatorNode CreateAnd()
        {
            return new AndOperatorNode();
        }
        public static OrOperatOrNode CreateOr()
        {
            return new OrOperatOrNode();
        }

        public static NotOperatorNode CreateNot()
        {
            return new NotOperatorNode();
        }

        public static AddOperatorNode CreateAdd()
        {
            return  new AddOperatorNode();
        }

        public static SubtractOperatorNode CreateReduce()
        {
            return new SubtractOperatorNode();
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
            return new CompareEquelOperatorNode();
           
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


        public static BreakNode  CreateBreak()
        {
            return new BreakNode();
            
        }
        
        public static ContinueNode CreateContinue()
        {
            return new ContinueNode();
           
        }

        public static AssignmentOperatorNode CreateAssign()
        {
            return new AssignmentOperatorNode();
           
        }

        public static CallNode CreateCall()
        {
            return new CallNode();
            
        }

        public static  AddAsignmentNode CreateAddAssignment()
        {
            return new AddAsignmentNode();
        }

        public static SubtractAsignmentNode CreateSubtractAssignment()
        {
            return new SubtractAsignmentNode();
        }

        public static ModAsignmentOperatorNode CreateModAssignment()
        {
            return new ModAsignmentOperatorNode();
        }

        public static MutiplyAsignOperatorNode CreateMultiplyAssignment()
        {
            return new MutiplyAsignOperatorNode();
        }

        public static DevideAsignOperatorNode CreateDevideAsignmentOperatorNode()
        {
            return new DevideAsignOperatorNode();
        }

        public static  ReturnOperatorNode CreateReturn()
        {
            return new ReturnOperatorNode();
        }

    }
}
