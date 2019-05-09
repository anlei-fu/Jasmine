using GrammerTest.Grammer.Scopes;
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

        public static AssignmentOperatorNode CreateAssigment(Block block)
        {
            return new AssignmentOperatorNode(block);
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

        public static IncrementNode  CreateIncrement(Block block)
        {
            return new IncrementNode(block);
            
        }

        public static DecrementNode CreateDecremnet(Block block)
        {
            return new DecrementNode(block);
            
        }

        public static NewOperatorNode CreateNew()
        {
            return new NewOperatorNode();
         
        }


        public static BreakNode  CreateBreak(BreakableBlock block)
        {
            return new BreakNode(block);
            
        }
        
        public static ContinueNode CreateContinue(BreakableBlock block)
        {
            return new ContinueNode(block);
           
        }

        public static AssignmentOperatorNode CreateAssign(Block block)
        {
            return new AssignmentOperatorNode(block);
           
        }

        public static CallNode CreateCall(Block block)
        {
            return new CallNode(block);
            
        }

        public static  AddAsignmentNode CreateAddAssignment(Block block)
        {
            return new AddAsignmentNode(block);
        }

        public static SubtractAsignmentNode CreateSubtractAssignment(Block block)
        {
            return new SubtractAsignmentNode(block);
        }

        public static ModAsignmentOperatorNode CreateModAssignment(Block block)
        {
            return new ModAsignmentOperatorNode(block);
        }

        public static MutiplyAsignOperatorNode CreateMultiplyAssignment(Block block)
        {
            return new MutiplyAsignOperatorNode(block);
        }

        public static DevideAsignOperatorNode CreateDevideAsignmentOperatorNode(Block block)
        {
            return new DevideAsignOperatorNode(block);
        }

        public static  ReturnOperatorNode CreateReturn(Block block)
        {
            return new ReturnOperatorNode(block);
        }

    }
}
