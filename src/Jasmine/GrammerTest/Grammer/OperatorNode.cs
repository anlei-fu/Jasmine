using GrammerTest.Grammer;
using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public abstract class OperatorNode:Excutor
    {
        public abstract OperatorResultType ResultType { get; }
        public Scope Scope { get; set; }
        public JObject Output { get; set; }
        public OperatorNode Parent { get; set; }
        public bool Excuted { get; protected set; }
        public List<Operand> Operands { get; set; }
        protected JObject getOperandObject(Operand operand)
        {
            if (operand.IsOperator)
            {
                operand.Operator.Excute();

                return operand.Operator.Output;
            }

            return operand.Object;
        }
    }

    public class Operand
    {
        public bool IsOperator { get; set; }
        public JObject Object { get; set; }
        public OperatorNode Operator { get; set; }
    }
    
    public abstract class SingleOperatorNode:OperatorNode
    {
        public sealed override void Excute()
        {
            excuteSingle(getOperandObject(Operands[0]));
        }

        protected abstract void excuteSingle(JObject obj);
        
    }

    public abstract class BinaryOperatorNode:OperatorNode
    {
       

        public sealed override void Excute()
        {
            excuteBinary(getOperandObject(Operands[0]), getOperandObject(Operands[1]));
        }

        protected abstract void excuteBinary(JObject obj1,JObject obj2);
    }

    public abstract class BoolOperatorNode:BinaryOperatorNode
    {
        public override OperatorResultType ResultType => OperatorResultType.Bool;
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

    public class AndOperatorNode : BoolOperatorNode
    {
        protected override bool caculate(bool flag1, bool flag2)
        {
            return flag1 && flag2;
        }
    }

    public class OrOperatorNode:BoolOperatorNode
    {
        protected override bool caculate(bool flag1, bool flag2)
        {
            return flag1 || flag2;
        }
    }
    public class NotOperatorNode : SingleOperatorNode
    {
        public override OperatorResultType ResultType => OperatorResultType.Bool;
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
        public override OperatorResultType ResultType => OperatorResultType.Variable;
        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            var jstring = obj2 as JString;

            if(obj1==null)
            {
                //throw a exception ,variable must be declared and property can dynamic add or remove
            }
            else
            {
                var obj = obj1.GetItem(jstring.Value);

                if(obj==null)
                {
                    obj = new JObject();
                }

                obj1.AddItem(jstring.Value, obj);

                Output = obj;
            }
        }
    }

    public class DeclareOperator : SingleOperatorNode
    {
        public override OperatorResultType ResultType => OperatorResultType.Variable;

        protected override void excuteSingle(JObject obj)
        {
            var jstring = obj as JString;

            Output = Scope.Declare(jstring.Value);
        }
    }

    public class AssignmentOperatorNode : BinaryOperatorNode
    {
        public override OperatorResultType ResultType => OperatorResultType.None;

        protected override void excuteBinary(JObject obj1, JObject obj2)
        {
            obj1 = obj2;
        }
    }
}
