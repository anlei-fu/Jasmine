using GrammerTest.Grammer;
using GrammerTest.Grammer.AstTree;
using GrammerTest.Grammer.AstTree.Exceptions;
using GrammerTest.Grammer.Scopes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Jasmine.Spider.Grammer
{
    public abstract class AstNode
    {
        public abstract string Name { get; }
        public JObject Output { get; internal set; }
        public abstract OutputType OutputType { get; }

        /// <summary>
        /// check operand's count or types is match
        /// </summary>
        public abstract void DoCheck();
        /// <summary>
        /// do operate
        /// </summary>
        public abstract void Excute();

        protected void trowOutputTypeIncorrectError()
        {
            throw new IncorrectOperatorException();
        }
    }

    /// <summary>
    /// operator node
    /// 
    /// ast tree build and connect with  operator nodes
    /// </summary>
    public abstract class OperatorNode : AstNode, IExcutor
    {
        public override string Name => OperatorType.Tostring0();
        public abstract OperatorType OperatorType { get; }
        /// <summary>
        /// operands
        /// </summary>
        public List<AstNode> Operands { get; set; } = new List<AstNode>();

        /// <summary>
        /// get  jobjcts that excuting needs
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected JObject getOperand(AstNode node)
        {
            if (node is OperandNode)
                return node.Output;

            node.Excute();

           

            return node.Output;
        }

       

        public virtual bool NeedExcute => true;
    }

    /// <summary>
    /// operand node , belong to operator
    /// </summary>
    public sealed class OperandNode : OperatorNode
    {

        public OperandNode(JObject obj)
        {
            Output = obj;
        }
        public override OutputType OutputType => OutputType.Object;

        public override bool NeedExcute => false;

        public override OperatorType OperatorType => OperatorType.Operand;

        public override string Name => "Operand";

        public override void DoCheck()
        {
            //ignore
        }

        public override void Excute()
        {
            //ignore
        }
    }





    /// <summary>
    /// only one operand
    /// </summary>
    public abstract class SingleOperatorNode : OperatorNode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sealed override void Excute()
        {
            excuteSingle(getOperand(Operands[0]));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void excuteSingle(JObject obj);

    }

    /// <summary>
    /// tow operands
    /// </summary>
    public abstract class BinaryOperatorNode : OperatorNode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sealed override void Excute()
        {
            excuteBinary(getOperand(Operands[0]), getOperand(Operands[1]));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void excuteBinary(JObject obj1, JObject obj2);
    }
    /// <summary>
    /// two operands's output are both jbool
    /// </summary>
    public abstract class LogicOperatorNode : BinaryOperatorNode
    {
        public override OutputType OutputType => OutputType.Bool;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void DoCheck()
        {
            if (!Operands[0].OutputType.IsBool() ||! Operands[1].OutputType.IsBool())
            {
                trowOutputTypeIncorrectError();
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            Output = caculate((JBool)obj1, (JBool)obj2);

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract JBool caculate(JBool flag1, JBool flag2);

    }
    /// <summary>
    /// &&
    /// </summary>
    public class AndOperatorNode : LogicOperatorNode
    {
        public override OperatorType OperatorType =>OperatorType.Add;

        public override string Name => throw new System.NotImplementedException();

        protected override JBool caculate(JBool para1, JBool para2)
        {
            return new JBool(para1.Value && para2.Value);
        }
    }
    /// <summary>
    /// ||
    /// </summary>
    public class OrOperatOrNode : LogicOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Or;

        protected override JBool caculate(JBool para1, JBool para2)
        {
            return new JBool(para1.Value || para2.Value);
        }
    }
    /// <summary>
    /// !
    /// </summary>
    public class NotOperatorNode : SingleOperatorNode
    {
        public override OutputType OutputType => OutputType.Bool;

        public override OperatorType OperatorType => OperatorType.Not;

        public override void DoCheck()
        {
            if (!Operands[0].OutputType.IsBool())
                trowOutputTypeIncorrectError();
        }

        protected override void excuteSingle(JObject obj)
        {
            Output = new JBool(!((JBool)obj).Value);
        }
    }

    /// <summary>
    /// 
    /// r.name
    /// jobject, name(string)
    /// </summary>
    public class MemberOperaterNode : BinaryOperatorNode
    {
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.MemberAccess;

        public override void DoCheck()
        {
            if (!Operands[1].OutputType.IsString())
                trowOutputTypeIncorrectError();
        }

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            if(Operands[1].Output is JMappingObject jm)
            {
                Output = jm.GetProperty((string)obj2);
            }
            else
            {
                Output = obj1.GetProperty((string)obj2);
            }

        }
    }

    public class DeclareAsignmentNode : OperatorNode
    {
        public DeclareAsignmentNode(Block block)
        {
            Block = block;
        }
        public Block Block { get; set; }
        public override OutputType OutputType => OutputType.None;

        public override OperatorType OperatorType => OperatorType.Assignment;

        public override void DoCheck()
        {
            
        }

        public override void Excute()
        {

            var result = getOperand(Operands[Operands.Count - 1]);

            Block.Declare((string)getOperand(Operands[0]), result);
         }
    }
    public class DeclareOperator : OperatorNode
    {
        public DeclareOperator(Block block)
        {
            Block = block;
        }
        public Block Block { get; }
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.Declare;

        public override void DoCheck()
        {
            if(Operands.Count==0)
            {

            }
        }

        public override void Excute()
        {
            foreach (var item in Operands)
            {
                Block.Declare((string)item.Output);
            }
          
        }
    }




    public class AssignmentOperatorNode : BinaryOperatorNode
    {
        public AssignmentOperatorNode(Block block)
        {
            Block = block;
        }
        public Block Block { get; set; }
        public override OutputType OutputType => OutputType.None;

        public override OperatorType OperatorType => OperatorType.Assignment;

        public override void DoCheck()
        {
            if (Operands[0].OutputType != OutputType.Object)
                trowOutputTypeIncorrectError();
        }

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            if(obj1 is JMappingObject jm)
            {
                jm.SetProperty(jm.Name, obj2.GetObject());
            }
            else
            {
                if (obj1.HasParent)
                    obj1.Parent.SetProperty(obj1.Name, obj2);

               
            }
        }
    }



    public abstract class BinaryNumberOperatorNode : BinaryOperatorNode
    {
        public override OutputType OutputType => OutputType.Number;

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            Output = caulate((JNumber)obj1, (JNumber)obj2);
        }

        protected abstract JNumber caulate(JNumber param1, JNumber param2);

        public override void DoCheck()
        {
            if (!Operands[0].OutputType.IsNumber() || !Operands[1].OutputType.IsNumber())
                trowOutputTypeIncorrectError();
        }
    }
    public class AddOperatorNode : BinaryNumberOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Add;

        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber(param1.Value + param2.Value);
        }
    }
    public class SubtractOperatorNode : BinaryNumberOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Subtract;

        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber(param1.Value - param2.Value);
        }
    }

    public class MutiplyOperatorNode : BinaryNumberOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Mutiply;

        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber(param1.Value * param2.Value);
        }
    }

    public class DevideOperatorNode : BinaryNumberOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Devide;

        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber(param1.Value / param2.Value);
        }
    }

    public class ModOperatorNode : BinaryNumberOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Mod;

        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber((int)(param1.Value) % (int)(param2.Value));
        }
    }


    public abstract class AsignmentNumberOperatorNode : AssignmentOperatorNode
    {
        public AsignmentNumberOperatorNode(Block block) : base(block)
        {
        }

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            var result = caculate((JNumber)obj1, (JNumber)obj2);


            if (obj1 is JMappingObject jm)
            {
                jm.SetProperty(jm.Name, result.GetObject());
            }
            else
            {
                obj1.SetProperty("", result);
            }
        }

        protected abstract JNumber caculate(JNumber num1, JNumber num2);
    }
    public class AddAsignmentNode : AsignmentNumberOperatorNode
    {
        public AddAsignmentNode(Block block) : base(block)
        {
        }

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }

        protected override JNumber caculate(JNumber num1, JNumber num2)
        {
            return new JNumber(num1.Value + num2.Value);
        }
    }
    public class SubtractAsignmentNode : AsignmentNumberOperatorNode
    {
        public SubtractAsignmentNode(Block block) : base(block)
        {
        }

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }

        protected override JNumber caculate(JNumber num1, JNumber num2)
        {
            return new JNumber(num1.Value -num2.Value);
        }
    }
    public class MutiplyAsignOperatorNode : AsignmentNumberOperatorNode
    {
        public MutiplyAsignOperatorNode(Block block) : base(block)
        {
        }

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }

        protected override JNumber caculate(JNumber num1, JNumber num2)
        {
            return new JNumber(num1.Value * num2.Value);
        }
    }

    public class DevideAsignOperatorNode : AsignmentNumberOperatorNode
    {
        public DevideAsignOperatorNode(Block block) : base(block)
        {
        }

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }

        protected override JNumber caculate(JNumber num1, JNumber num2)
        {
            return new JNumber(num1.Value / num2.Value);
        }
    }

    public class ModAsignmentOperatorNode : AsignmentNumberOperatorNode
    {
        public ModAsignmentOperatorNode(Block block) : base(block)
        {
        }

        public override void DoCheck()
        {
        }

        protected override JNumber caculate(JNumber num1, JNumber num2)
        {
            return new JNumber((int)num1.Value%(int)num2.Value);
        }
    }


    public abstract class BinaryNumerLogicOpeatorNode : BinaryOperatorNode
    {
        public override OutputType OutputType => OutputType.Bool;

        public override void DoCheck()
        {
            if (!Operands[0].OutputType.IsNumber() || !Operands[1].OutputType.IsNumber())
                trowOutputTypeIncorrectError();
        }
        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            Output = caculate(((JNumber)obj1), ((JNumber)obj2));
        }


        protected abstract JBool caculate(JNumber number1, JNumber number2);

    }

    public class BiggerOperatorNode : BinaryNumerLogicOpeatorNode
    {
        public override OperatorType OperatorType => OperatorType.Bigger;

        protected override JBool caculate(JNumber number1, JNumber number2)
        {
            return new JBool(number1 > number2);
        }
    }

    public class BiggerEquelOperatorNode : BinaryNumerLogicOpeatorNode
    {
        public override OperatorType OperatorType => OperatorType.BiggerEquel;

        protected override JBool caculate(JNumber number1, JNumber number2)
        {
            return new JBool(number1 >= number2);
        }
    }

    public class LessOperatorNode : BinaryNumerLogicOpeatorNode
    {
        public override OperatorType OperatorType => OperatorType.Less;

        protected override JBool caculate(JNumber number1, JNumber number2)
        {
            return new JBool(number1 < number2);
        }
    }


    public class LessEquelOperatorNode : BinaryNumerLogicOpeatorNode
    {
        public override OperatorType OperatorType => OperatorType.LessEquel;

        protected override JBool caculate(JNumber number1, JNumber number2)
        {
            return new JBool(number1 < number2);
        }
    }

    public abstract class CompareNode : BinaryOperatorNode
    {
        public override OutputType OutputType => OutputType.Bool;

        public override void DoCheck()
        {
        }
    }

    public class CompareEquelOperatorNode : CompareNode
    {
       

        public override OperatorType OperatorType =>OperatorType.Equel;

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            Output = new JBool(obj1.Equals(obj2));
        }
    }

    public class MinusOperatorNode : SingleOperatorNode
    {
        public override OutputType OutputType =>OutputType.Number;

        public override OperatorType OperatorType => OperatorType.NotEquel;

        public override void DoCheck()
        {
            if (!Operands[0].OutputType.IsNumber())
                trowOutputTypeIncorrectError();
        }

        protected override void excuteSingle(JObject obj)
        {
            Output = new JNumber((0 - ((JNumber)obj).Value));
        }
    }


    public class ComareNotEquelNode : CompareNode
    {
        public override OperatorType OperatorType => OperatorType.NotEquel;

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            Output = new JBool(obj1.Equals(obj2));
        }
    }

    public class IncrementNode : SingleOperatorNode
    {
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.Increment;

        public override void DoCheck()
        {
            
        }

        protected override void excuteSingle(JObject obj)
        {
            if(obj is JMappingObject jm)
            {
                jm.SetProperty(jm.Name, ((JNumber)obj).Value += 1);
            }
            else
            {
                obj.SetProperty(obj.Name,new JNumber((((JNumber)obj).Value += 1)));
            }
        }
    }

    public class DecrementNode : SingleOperatorNode
    {
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.Decrement;

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }

        protected override void excuteSingle(JObject obj)
        {
            if (obj is JMappingObject jm)
            {
                jm.SetProperty(jm.Name, ((JNumber)obj).Value -= 1);
            }
            else
            {
                obj.SetProperty(obj.Name, new JNumber((((JNumber)obj).Value -= 1)));
            }
        }
    }


    public class NewOperatorNode : SingleOperatorNode
    {
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.NewInstance;

        public override void DoCheck()
        {
        }

        protected override void excuteSingle(JObject obj)
        {
        }
    }

    public class ArrayIndexOperatorNode : BinaryOperatorNode
    {
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType =>OperatorType.ArrayIndex;

        public override void DoCheck()
        {
        }

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
        }
    }

    public class QueryScopeOperatorNode : OperatorNode
    {

        public QueryScopeOperatorNode(string name,Block block)
        {
            ObjectName = name;
            Block = block;
        }
        public Block Block { get; }
        public string ObjectName { get; set; }
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.QueryObject;

        public override void DoCheck()
        {
            
        }

        public override void Excute()
        {
            Output = Block.GetVariable(ObjectName);
        }

     
    }
    public class CallNode : OperatorNode
    {
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.Call;

        public override void DoCheck()
        {
            
        }

        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }

    public class FunctionDefineNode : OperatorNode
    {
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.Function;

        public override void DoCheck()
        {
        }

        public override void Excute()
        {
        }
    }

    public class BreakNode : OperatorNode
    {
        public override OutputType OutputType => OutputType.None;

        public override OperatorType OperatorType => OperatorType.Break;

        public override void DoCheck()
        {
          
        }

        public override void Excute()
        {
        }
    }

    public class ContinueNode:BreakNode
    {
        public override OperatorType OperatorType => OperatorType.Continue;
        public override void Excute()
        {
            base.Excute();
        }
    }

    public class ReturnOperatorNode : SingleOperatorNode
    {
        public override OperatorType OperatorType =>OperatorType.Return;

        public override OutputType OutputType => OutputType.Object;

        public override void DoCheck()
        {
        }

        protected override void excuteSingle(JObject obj)
        {
        }
    }

    public class TernaryOperatorNode : OperatorNode
    {
        public override OperatorType OperatorType => throw new System.NotImplementedException();

        public override OutputType OutputType => throw new System.NotImplementedException();

        public override void DoCheck()
        {
        }

        public override void Excute()
        {
        }
    }
}
