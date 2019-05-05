using GrammerTest.Grammer;
using GrammerTest.Grammer.AstTree;
using GrammerTest.Grammer.Scopes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Jasmine.Spider.Grammer
{
    public abstract class AstNode
    {
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

        }
    }

    /// <summary>
    /// operator node
    /// 
    /// ast tree build and connect with  operator nodes
    /// </summary>
    public abstract class OperatorNode : AstNode, IExcutor
    {
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
            var jstring = obj2 as JString;

            if (obj1 == null)
            {
                //throw a exception ,variable must be declared and property can dynamic add or remove
            }
            else
            {
                if (obj1 is JMappingObject mapping)
                {

                    var result = mapping.GetProperty(jstring.Value);

                    Output = result;
                }
                else
                {
                    var result = obj1.GetProperty((string)jstring);

                    Output = result;
                }
            }
        }
    }

    public class DeclareAsignmentNode : OperatorNode
    {
        public override OutputType OutputType => OutputType.None;

        public override OperatorType OperatorType => OperatorType.Assignment;

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }

        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
    public class DeclareOperator : SingleOperatorNode
    {
        public Block Scope { get; }
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.Declare;

        public override void DoCheck()
        {
          
        }

        protected override void excuteSingle(JObject obj)
        {
            
        }
    }




    public class AssignmentOperatorNode : OperatorNode
    {
        public override OutputType OutputType => OutputType.None;

        public override OperatorType OperatorType => OperatorType.Assignment;

        public override void DoCheck()
        {
            if (Operands[0].OutputType != OutputType.Object)
                trowOutputTypeIncorrectError();
        }

        public override void Excute()
        {
            // reference  exception
            Operands[1].Output = Operands[2].Output;
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
            return new JNumber(param1.Value + param1.Value);
        }
    }
    public class SubtractOperatorNode : BinaryNumberOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Subtract;

        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber(param1.Value - param1.Value);
        }
    }

    public class MutiplyOperatorNode : BinaryNumberOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Mutiply;

        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber(param1.Value * param1.Value);
        }
    }

    public class DevideOperatorNode : BinaryNumberOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Devide;

        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber(param1.Value / param1.Value);
        }
    }

    public class ModOperatorNode : BinaryNumberOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Mod;

        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber((int)(param1.Value) % (int)(param1.Value));
        }
    }


    public class AsignmentNumberOperatorNode : AssignmentOperatorNode
    {

    }
    public class AddAsignmentNode : AsignmentNumberOperatorNode
    {
        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }




    }
    public class SubtractAsignmentNode : AsignmentNumberOperatorNode
    {
        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }


    }
    public class MutiplyAsignOperatorNode : AsignmentNumberOperatorNode
    {
        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }




    }

    public class DevideAsignOperatorNode : AsignmentNumberOperatorNode
    {
        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }




    }

    public class ModAsignmentOperatorNode : AsignmentNumberOperatorNode
    {
        public override void DoCheck()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
    }

    public class BiggerEquelOperatorNode : BinaryNumerLogicOpeatorNode
    {
        public override OperatorType OperatorType => OperatorType.BiggerEquel;

        protected override JBool caculate(JNumber number1, JNumber number2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class LessOperatorNode : BinaryNumerLogicOpeatorNode
    {
        public override OperatorType OperatorType => OperatorType.Less;

        protected override JBool caculate(JNumber number1, JNumber number2)
        {
            throw new System.NotImplementedException();
        }
    }


    public class LessEquelOperatorNode : BinaryNumerLogicOpeatorNode
    {
        public override OperatorType OperatorType => OperatorType.LessEquel;

        protected override JBool caculate(JNumber number1, JNumber number2)
        {
            throw new System.NotImplementedException();
        }
    }

    public abstract class CompareNode : BinaryOperatorNode
    {
        public override OutputType OutputType => OutputType.Bool;

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }
    }

    public class CompareEquelOperatorNode : CompareNode
    {
       

        public override OperatorType OperatorType =>OperatorType.Equel;

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
    }


    public class ComareNotEquelNode : CompareNode
    {
        public override OperatorType OperatorType => OperatorType.NotEquel;

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
    }

    public class DcrementNode : SingleOperatorNode
    {
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.Decrement;

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }

        protected override void excuteSingle(JObject obj)
        {
            throw new System.NotImplementedException();
        }
    }


    public class NewOperatorNode : SingleOperatorNode
    {
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.NewInstance;

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }

        protected override void excuteSingle(JObject obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ArrayIndexOperatorNode : BinaryOperatorNode
    {
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType =>OperatorType.ArrayIndex;

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class QueryScopeOperatorNode : SingleOperatorNode
    {

        public QueryScopeOperatorNode(string name)
        {
            ObjectName = name;
        }

        public string ObjectName { get; set; }
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.QueryObject;

        public override void DoCheck()
        {
            
        }

        protected override void excuteSingle(JObject obj)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public override void Excute()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        protected override void excuteSingle(JObject obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class TernaryOperatorNode : OperatorNode
    {
        public override OperatorType OperatorType => throw new System.NotImplementedException();

        public override OutputType OutputType => throw new System.NotImplementedException();

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }

        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }
}
