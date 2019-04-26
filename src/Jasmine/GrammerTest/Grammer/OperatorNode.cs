using GrammerTest.Grammer;
using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public abstract class OperatorNode:Excutor
    {
        public virtual bool IsOperator { get; } 
        public abstract OutputType OutputType { get; }
        public Scope Scope { get; set; }
        public JObject Output { get; set; }
        public OperatorNode Parent { get; set; }
        public OperatorType OperatorType { get; set; }
        public bool Excuted { get; protected set; }
        public bool NeedExcute { get; set; }
        public List<OperatorNode> Children { get; set; }
        protected JObject getOperandObject(OperatorNode node)
        {
            if (node.NeedExcute)
                node. Excute();

            return node.Output;
        }

        public abstract void DoCheck();
    }


    public sealed class OperandNode : OperatorNode
    {
        public OperandNode(JObject obj)
        {
            Output = obj;
        }
        public override OutputType OutputType => throw new System.NotImplementedException();
        public override void Excute()
        {
           
        }
    }



    public abstract class NoneOperatorNode:OperatorNode
    {
        public override OutputType OutputType => throw new System.NotImplementedException();

        
    }
    
    /// <summary>
    /// only one operand
    /// </summary>
    public abstract class SingleOperatorNode:OperatorNode
    {
        public sealed override void Excute()
        {
            excuteSingle(getOperandObject(Children[0]));
        }

        protected abstract void excuteSingle(JObject obj);
        
    }

    /// <summary>
    /// tow operands
    /// </summary>
    public abstract class BinaryOperatorNode:OperatorNode
    {
       

        public sealed override void Excute()
        {
            excuteBinary(getOperandObject(Children[0]), getOperandObject(Children[1]));
        }

        protected abstract void excuteBinary(JObject obj1,JObject obj2);
    }



    public abstract class LogicOperatorNode:BinaryOperatorNode
    {
        public override OutputType OutputType => OutputType.Bool;
        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            var result = caculate(((JBool)obj1).Value, ((JBool)obj2).Value);

            if (Output == null)
                Output = new JBool(result);
            else
                ((JBool)Output).Value = result;

        }
        protected abstract bool caculate(bool flag1, bool flag2);
       
    }

    public class AndOperatorNode : LogicOperatorNode
    {
        protected override bool caculate(bool flag1, bool flag2)
        {
            return flag1 && flag2;
        }
    }

    public class OrOperatorNode:LogicOperatorNode
    {
        protected override bool caculate(bool flag1, bool flag2)
        {
            return flag1 || flag2;
        }
    }
    public class NotOperatorNode : SingleOperatorNode
    {
        public override OutputType OutputType => OutputType.Bool;
        protected override void excuteSingle(JObject obj)
        {
            var jbool = obj as JBool;

            jbool.Value = !jbool.Value;

            if (Output == null)
                Output = new JBool(jbool.Value);
            else
                ((JBool)Output).Value = jbool.Value;
        }
    }

    /// <summary>
    /// 
    /// r.name
    /// jobject, name(string)
    /// </summary>
    public class MemberOperaterNode : BinaryOperatorNode
    {
        public override OutputType OutputType => OutputType.Variable;
        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            var jstring = obj2 as JString;

            if(obj1==null)
            {
                //throw a exception ,variable must be declared and property can dynamic add or remove
            }
            else
            {
                if(obj1 is JMappingObject mapping)
                {

                    var result=  mapping.GetProperty(jstring.Value);

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

    public class DeclareOperator : SingleOperatorNode
    {
        public override OutputType OutputType => OutputType.Variable;

        protected override void excuteSingle(JObject obj)
        {
            var jstring = obj as JString;

            Output = Scope.Declare(jstring.Value);
        }
    }

    public class AssignmentOperatorNode : BinaryOperatorNode
    {
        public override OutputType OutputType => OutputType.None;

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            obj1 = obj2;
        }
    }

    public abstract class AsignmentNumberOperatorNode:AssignmentOperatorNode
    {
        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            caculate(obj1,((JNumber)obj1).Value ,((JNumber)obj2).Value);
        }

        protected abstract void caculate(JObject obj1,double param1,  double param2);
    }

    public abstract class BinaryNumberOperatorNode : BinaryOperatorNode
    {
        public override OutputType OutputType => OutputType.Number;

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            throw new System.NotImplementedException();
        }

        protected abstract float caulate(float param1, float param2);


       
    }
    public class AddOperatornNode : BinaryNumberOperatorNode
    {
        protected override float caulate(float param1, float param2)
        {
            return param1 + param2;
        }
    }
    public class ReduceOperatorNode : BinaryNumberOperatorNode
    {
        protected override float caulate(float param1, float param2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MutiplyOperatorNode : BinaryNumberOperatorNode
    {
        protected override float caulate(float param1, float param2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class DevideOperatorNode : BinaryNumberOperatorNode
    {
        public override OutputType OutputType => throw new System.NotImplementedException();

        protected override float caulate(float param1, float param2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ModOperatorNode : BinaryNumberOperatorNode
    {
        public override OutputType OutputType => throw new System.NotImplementedException();

        protected override float caulate(float param1, float param2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class AddAsignmentNode : AsignmentNumberOperatorNode
    {
        protected override void caculate(JObject obj1, float param1, float param2)
        {
            throw new System.NotImplementedException();
        }
    }
    public class ReduceAsignmentNode : AsignmentNumberOperatorNode
    {
        protected override void caculate(JObject obj1, float param1, float param2)
        {
            throw new System.NotImplementedException();
        }
    }
    public class MutiplyAsignOperatorNode : AsignmentNumberOperatorNode
    {
        protected override void caculate(JObject obj1, float param1, float param2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class DevideAsignOperatorNode : AsignmentNumberOperatorNode
    {
        protected override void caculate(JObject obj1, float param1, float param2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ModAsignmentOperatorNode : AsignmentNumberOperatorNode
    {
        protected override void caculate(JObject obj1, float param1, float param2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class BiggerOperatorNode : BinaryNumberOperatorNode
    {
        protected override float caulate(float param1, float param2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class BiggerEquelOperatorNode : BinaryNumberOperatorNode
    {
        protected override float caulate(float param1, float param2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class LessOperatorNode : BinaryNumberOperatorNode
    {
        protected override float caulate(float param1, float param2)
        {
            throw new System.NotImplementedException();
        }
    }


    public class LessEquelOperatorNode : BinaryNumberOperatorNode
    {
        protected override float caulate(float param1, float param2)
        {
            throw new System.NotImplementedException();
        }
    }

    public abstract class CompareNode:BinaryOperatorNode
    {

    }

    public class CompareEquelOperatorNode : CompareNode
    {
        public override OutputType OutputType => throw new System.NotImplementedException();

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ComareNotEquelNode : CompareNode
    {
        public override OutputType OutputType => throw new System.NotImplementedException();

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class IncrementNode:OperatorNode
    {

    }

    public class DcrementNode:OperatorNode
    {

    }
  

    public class NewOperatorNode:OperatorNode
    {

    }
}
