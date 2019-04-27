using GrammerTest.Grammer;
using GrammerTest.Grammer.AstTree;
using System.Collections.Generic;

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
        /// <summary>
        /// operands
        /// </summary>
        public List<AstNode> Operands { get; set; }
        
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

           

            return null;
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
        public override OutputType OutputType => throw new System.NotImplementedException();

        public override bool NeedExcute => false;

        public override void DoCheck()
        {
            
        }

        public override void Excute()
        {
          
        }
    }





    /// <summary>
    /// only one operand
    /// </summary>
    public abstract class SingleOperatorNode : OperatorNode
    {
        public sealed override void Excute()
        {
            excuteSingle(getOperand(Operands[0]));
        }

        protected abstract void excuteSingle(JObject obj);

    }

    /// <summary>
    /// tow operands
    /// </summary>
    public abstract class BinaryOperatorNode : OperatorNode
    {
        public sealed override void Excute()
        {
            excuteBinary(getOperand(Operands[0]), getOperand(Operands[1]));
        }

        protected abstract void excuteBinary(JObject obj1, JObject obj2);
    }
    /// <summary>
    /// two operands's output are both jbool
    /// </summary>
    public abstract class LogicOperatorNode : BinaryOperatorNode
    {
        public override OutputType OutputType => OutputType.Bool;

        public override void DoCheck()
        {
            if (!Operands[0].OutputType.IsBool() ||! Operands[1].OutputType.IsBool())
            {
                trowOutputTypeIncorrectError();
            }
               
            


        }
        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            Output = caculate((JBool)obj1, (JBool)obj2);

        }

        protected abstract JBool caculate(JBool flag1, JBool flag2);

    }
    /// <summary>
    /// &&
    /// </summary>
    public class AndOperatorNode : LogicOperatorNode
    {


        protected override JBool caculate(JBool para1, JBool para2)
        {
            return new JBool(para1.Value && para2.Value);
        }
    }
    /// <summary>
    /// ||
    /// </summary>
    public class OrOperatorNode : LogicOperatorNode
    {
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
    public class DeclareOperator : SingleOperatorNode
    {
        public Scope Scope { get; }
        public override OutputType OutputType => OutputType.Object;

        public override void DoCheck()
        {
          
        }

        protected override void excuteSingle(JObject obj)
        {
            Output = Scope.Declare((string)obj);
        }
    }




    public class AssignmentOperatorNode : OperatorNode
    {
        public override OutputType OutputType => OutputType.None;

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
    public class AddOperatornNode : BinaryNumberOperatorNode
    {
        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber(param1.Value + param1.Value);
        }
    }
    public class ReduceOperatorNode : BinaryNumberOperatorNode
    {
        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber(param1.Value - param1.Value);
        }
    }

    public class MutiplyOperatorNode : BinaryNumberOperatorNode
    {
        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber(param1.Value * param1.Value);
        }
    }

    public class DevideOperatorNode : BinaryNumberOperatorNode
    {
        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber(param1.Value / param1.Value);
        }
    }

    public class ModOperatorNode : BinaryNumberOperatorNode
    {
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
    public class ReduceAsignmentNode : AsignmentNumberOperatorNode
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


        protected override JBool caculate(JNumber number1, JNumber number2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class BiggerEquelOperatorNode : BinaryNumerLogicOpeatorNode
    {
        protected override JBool caculate(JNumber number1, JNumber number2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class LessOperatorNode : BinaryNumerLogicOpeatorNode
    {
        protected override JBool caculate(JNumber number1, JNumber number2)
        {
            throw new System.NotImplementedException();
        }
    }


    public class LessEquelOperatorNode : BinaryNumerLogicOpeatorNode
    {
        protected override JBool caculate(JNumber number1, JNumber number2)
        {
            throw new System.NotImplementedException();
        }
    }

    public abstract class CompareNode : BinaryOperatorNode
    {
        public override OutputType OutputType => throw new System.NotImplementedException();

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }
    }

    public class CompareEquelOperatorNode : CompareNode
    {
        public override OutputType OutputType => throw new System.NotImplementedException();



        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MinusOperatorNode : SingleOperatorNode
    {
        public override OutputType OutputType =>OutputType.Number;

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
       
        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class IncrementNode : SingleOperatorNode
    {
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

    public class DcrementNode : SingleOperatorNode
    {
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


    public class NewOperatorNode : SingleOperatorNode
    {
        public override OutputType OutputType => throw new System.NotImplementedException();

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
        public override OutputType OutputType => throw new System.NotImplementedException();

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

        }
        public override OutputType OutputType => throw new System.NotImplementedException();

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }

        protected override void excuteSingle(JObject obj)
        {
            throw new System.NotImplementedException();
        }
    }
    public class CallNode : OperatorNode
    {
        public override OutputType OutputType => OutputType.Object;

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }

        public override void Excute()
        {
            throw new System.NotImplementedException();
        }
    }

    public class FunctionDefineNode : OperatorNode
    {
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
