using Jasmine.Interpreter.AstTree.Exceptions;
using Jasmine.Interpreter.Scopes;
using Jasmine.Interpreter.Tokenizers;
using Jasmine.Interpreter.TypeSystem;
using Jasmine.Reflection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Jasmine.Interpreter.AstTree
{
    public abstract class AstNode
    {
        public abstract string Name { get; }
        
        public abstract OutputType OutputType { get; }

        /// <summary>
        /// check operand's count or types is match
        /// </summary>
        public abstract void DoCheck();
        /// <summary>
        /// do operate
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract void Excute(ExcutingStack stack);

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
    public abstract class OperatorNode : AstNode
    {
        public override string Name => OperatorType.ToString0();
        public abstract OperatorType OperatorType { get; }
        /// <summary>
        /// operands
        /// </summary>
        public List<AstNode> Operands { get; set; } = new List<AstNode>();

       
      

       

 
    }

    /// <summary>
    /// operand node , belong to operator
    /// </summary>
    public sealed class OperandNode : OperatorNode
    {

        public OperandNode(Any obj)
        {
            Output = obj;
        }
        public Any Output { get; set; }
        public override OutputType OutputType => OutputType.Object;

        

        public override OperatorType OperatorType => OperatorType.Operand;

        public override string Name => "Operand";

        public override void DoCheck()
        {
            //ignore
        }

        public override void Excute(ExcutingStack stack)
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
        public sealed override void Excute(ExcutingStack stack)
        {
            //excuteSingle(getOperand(Operands[0],stack),stack);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void excuteSingle(Any obj1,ExcutingStack stack);

    }

    /// <summary>
    /// tow operands
    /// </summary>
    public abstract class BinaryOperatorNode : OperatorNode
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sealed override void Excute(ExcutingStack stack)
        {
            Operands[0].Excute(stack);
            Operands[1].Excute(stack);
            excuteBinary(stack.Get(Operands[0]),stack.Get(Operands[1]),stack);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void excuteBinary(Any obj1,Any obj2,ExcutingStack stack);
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
        protected override void excuteBinary(Any obj1,Any obj2,ExcutingStack stack)
        {
            stack.Push(this, caculate((JBool)obj1,(JBool)obj2));

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override JBool caculate(JBool para1, JBool para2)
        {
            if (!para1.Value)
                return new JBool(false);
            else if (!para2.Value)
                return new JBool(false);
            return new JBool(true);
        }
    }
    /// <summary>
    /// ||
    /// </summary>
    public class OrOperatOrNode : LogicOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Or;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override JBool caculate(JBool para1, JBool para2)
        {
            if (para1.Value)
                return new JBool(true);
            else if (para1.Value)
                return new JBool(true);
            else
                return new JBool(false);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void excuteSingle(Any obj1,ExcutingStack stack)
        {
            stack.Push(this, new JBool(!((JBool)stack.Get(Operands[0])).Value));
        }
    }

    /// <summary>
    /// 
    /// r.name
    /// jobject, name(string)
    /// </summary>
    public class MemberOperaterNode : OperatorNode
    {
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.MemberAccess;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void DoCheck()
        {
            if (!Operands[1].OutputType.IsString())
                trowOutputTypeIncorrectError();
        }

        public override void Excute(ExcutingStack stack)
        {
            Operands[0].Excute(stack);

            if (stack.Get(Operands[0]) is JMappingObject jm)
            {
                 stack.Push(this,jm.GetProperty(((QueryScopeOperatorNode)Operands[1]).ObjectName));
            }
            else
            {
                 stack.Push(this, ((IPropertyGetter)stack.Get(Operands[0])).GetProperty(((QueryScopeOperatorNode)Operands[1]).ObjectName));
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Excute(ExcutingStack stack)
        {
            Operands[Operands.Count - 1].Excute(stack);

            var name =(string)((OperandNode)Operands[0]).Output;

            if (stack.Get(Operands[Operands.Count-1]) is JMappingObject jmo)
            {
                if(BaseTypes.Base.Contains(jmo.InstanceType))
                {
                    Block.Declare(name, jmo.ToJObject());
                }
                else
                {
                    Block.Declare(name, jmo);
                }
            }
            else
            {

                Block.Declare(name, stack.Get(Operands[Operands.Count - 1]));
            }
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
                throw new AstGrammerException("");
            }
        }

        public override void Excute(ExcutingStack stack)
        {
            foreach (var item in Operands)
            {
                Block.Declare((string)((OperandNode)item).Output);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void excuteBinary(Any obj1, Any obj2,ExcutingStack stack)
        {
            if(obj1 is JMappingObject jm)
            {
                if (jm.Parent != null)
                {
                    /*
                     * must convert to double 
                     */ 
                    if (BaseTypes.Numbers.Contains(jm.InstanceType))
                    {
                        jm.SetProperty(JMappingObject.EnsureCorrectNumberType(jm.InstanceType, obj2.GetObject()));
                    }
                    else
                    {
                        jm.SetProperty(obj2.GetObject());
                    }
                }
                else
                {
                    Block.Reset(jm.Name, obj2);
                }
            }
            else
            {
                /*
                 *  must clone ,because jstring ,jnumber is class
                 */
                if (obj2 is JMappingObject jmo)
                {
                    /*
                     * 
                     *  clone a new jobject
                     */ 
                    if (BaseTypes.Base.Contains(jmo.InstanceType))
                        obj2 = jmo.ToJObject();
                }
                else
                {

                    if (obj2.Type != JType.Object)
                    {
                        obj2 = ((IClonable)obj2).Clone();
                    }
                }

                if (obj1.HasParent)
                {

                    obj1.Parent.SetProperty(obj1.Name, obj2);
                }
                else
                {
                    Block.Reset(obj1.Name, obj2);
                }
            }
        }
    }



    public abstract class BinaryNumberOperatorNode : BinaryOperatorNode
    {
        public override OutputType OutputType => OutputType.Number;

        protected override void excuteBinary(Any obj1, Any obj2,ExcutingStack stack)
        {
             stack.Push(this,caulate((JNumber)obj1, (JNumber)obj2));
        }

        protected abstract JNumber caulate(JNumber param1, JNumber param2);

        public override void DoCheck()
        {
            if (!Operands[0].OutputType.IsNumber() || !Operands[1].OutputType.IsNumber())
                trowOutputTypeIncorrectError();
        }
    }
    public class AddOperatorNode : BinaryOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Add;

        public override OutputType OutputType => throw new System.NotImplementedException();

        public override void DoCheck()
        {
           
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void excuteBinary(Any obj1, Any obj2,ExcutingStack stack)
        {
            if(obj1 is JMappingObject jm)
            {
                if(jm.InstanceType==BaseTypes.TString)
                {
                    stack.Push(this, new JString(Convert.ToString(jm.Instance) + (string)obj2));
                }
                else if(BaseTypes.Numbers.Contains(jm.InstanceType))
                {
                    stack.Push(this, new JNumber(Convert.ToDouble(jm.Instance) + (double)obj2));
                }
                else
                {
                    throw new OperatorRequiredTypeIncorrectException();
                }
                
            }
            else if(obj1 is JString jstr)
            {
                stack.Push(this, new JString(jstr.Value + (string)obj2));
            }
            else if(obj1 is JNumber jnum)
            {
                stack.Push(this,  new JNumber(jnum.Value + (double)obj2));
            }
            else
            {
                throw new OperatorRequiredTypeIncorrectException();
            }

        }
    }
    public class SubtractOperatorNode : BinaryNumberOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Subtract;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber(param1.Value - param2.Value);
        }
    }

    public class MutiplyOperatorNode : BinaryNumberOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Mutiply;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber(param1.Value * param2.Value);
        }
    }

    public class DevideOperatorNode : BinaryNumberOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Devide;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override JNumber caulate(JNumber param1, JNumber param2)
        {
            return new JNumber(param1.Value / param2.Value);
        }
    }

    public class ModOperatorNode : BinaryNumberOperatorNode
    {
        public override OperatorType OperatorType => OperatorType.Mod;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void excuteBinary(Any obj1, Any obj2,ExcutingStack stack)
        {
            var result = caculate((JNumber)obj1, (JNumber)obj2);


            if (obj1 is JMappingObject jm)
            {
                jm.SetProperty(result.GetObject());
            }
            else
            {
                obj1.Parent.SetProperty(obj1.Name, result);
            }
        }

        protected abstract JNumber caculate(JNumber num1, JNumber num2);
    }
    public class AddAsignmentNode :BinaryOperatorNode
    {
        public AddAsignmentNode(Block block) 
        {
            Block = block;
        }
        public Block Block { get; set; }

        public override OperatorType OperatorType => OperatorType.AddAsignment;

        public override OutputType OutputType => OutputType.Object;

        public override void DoCheck()
        {
          
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void excuteBinary(Any obj1, Any obj2,ExcutingStack stack)
        {
            if (obj1 is JMappingObject jm)
            {
              if(jm.InstanceType==BaseTypes.TString)
                {
                    jm.SetProperty(Convert.ToString(jm.Instance) + (string)obj2);
                }
              else if(BaseTypes.Numbers.Contains(jm.InstanceType))
                {
                    jm.SetProperty(Convert.ToDouble(jm.Instance) + (double)obj2);
                }
              else
                {
                    throw new OperatorRequiredTypeIncorrectException();
                }
            }
            else
            {
                if (obj2.Type != JType.Object)
                {
                    obj2 = ((IClonable)obj2).Clone();
                }

                if (obj1.HasParent)
                {
                    if (obj1 is JString jstr)
                    {

                        obj1.Parent.SetProperty(obj1.Name, new JString(jstr.Value+ (string)obj2));
                    }
                    else if(obj1 is JNumber jnum)
                    {
                        obj1.Parent.SetProperty(obj1.Name, new JNumber(jnum.Value + (double)obj2));
                    }
                    else
                    {
                        throw new OperatorRequiredTypeIncorrectException();
                    }
                }
                else
                {
                    if (obj1 is JString jstr)
                    {

                        obj1.Parent.SetProperty(obj1.Name, new JString((string)obj2+jstr.Value));
                    }
                    else if (obj1 is JNumber jnum)
                    {
                        obj1.Parent.SetProperty(obj1.Name, new JNumber((double)obj2 + jnum.Value));
                    }
                    else
                    {
                        throw new OperatorRequiredTypeIncorrectException();
                    }
                }
            }
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void excuteBinary(Any obj1, Any obj2,ExcutingStack stack)
        {
            stack.Push(this,caculate((JNumber)obj1, (JNumber)obj2));
        }


        protected abstract JBool caculate(JNumber number1, JNumber number2);

    }

    public class BiggerOperatorNode : BinaryNumerLogicOpeatorNode
    {
        public override OperatorType OperatorType => OperatorType.Bigger;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override JBool caculate(JNumber number1, JNumber number2)
        {
            return new JBool(number1 > number2);
        }
    }

    public class BiggerEquelOperatorNode : BinaryNumerLogicOpeatorNode
    {
        public override OperatorType OperatorType => OperatorType.BiggerEquel;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override JBool caculate(JNumber number1, JNumber number2)
        {
            return new JBool(number1 >= number2);
        }
    }

    public class LessOperatorNode : BinaryNumerLogicOpeatorNode
    {
        public override OperatorType OperatorType => OperatorType.Less;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override JBool caculate(JNumber number1, JNumber number2)
        {
            return new JBool(number1 < number2);
        }
    }


    public class LessEquelOperatorNode : BinaryNumerLogicOpeatorNode
    {
        public override OperatorType OperatorType => OperatorType.LessEquel;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void excuteBinary(Any obj1, Any obj2,ExcutingStack stack)
        {
            stack.Push(this, new JBool(obj1.Equals(obj2)));
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void excuteSingle(Any obj, ExcutingStack stack)
        {
             stack.Push(this, new JNumber(0 - ((JNumber)obj).Value));
        }
    }


    public class ComareNotEquelNode : CompareNode
    {
        public override OperatorType OperatorType => OperatorType.NotEquel;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void excuteBinary(Any obj1, Any obj2,ExcutingStack stack)
        {
            stack.Push(this, new JBool(!obj1.Equals(obj2)));
        }
    }

    public class IncrementNode : SingleOperatorNode
    {
        public IncrementNode(Block block)
        {
            Block = block;
        }
        public Block Block { get; set; }
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.Increment;

        public override void DoCheck()
        {
            
        }

        protected override void excuteSingle(Any obj,ExcutingStack stack)
        {
            if(obj is JMappingObject jm)
            {
                jm.SetProperty(((JNumber)obj).Value += 1);
            }
            else
            {
                var jmnu = new JNumber((((JNumber)obj).Value + 1));
                jmnu.Name = obj.Name;

                if (obj.HasParent)
                {
                    ((JObject)obj).SetProperty(obj.Name, jmnu);
                }
                else
                {
                    stack.Push(this,jmnu);

                    Block.Reset(obj.Name, jmnu);
                }
                        
            }
        }
    }

    public class DecrementNode : SingleOperatorNode
    {
        public DecrementNode(Block block)
        {
            Block = block;
        }
        public Block Block { get; set; }
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.Decrement;

        public override void DoCheck()
        {
            throw new System.NotImplementedException();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void excuteSingle(Any obj,ExcutingStack stack)
        {
            if (obj is JMappingObject jm)
            {
                jm.SetProperty(((JNumber)obj).Value -= 1);
            }
            else
            {
                var jmnu = new JNumber((((JNumber)obj).Value + 1));
                jmnu.Name = obj.Name;

                if (obj.HasParent)
                    obj.Parent.SetProperty(obj.Name, jmnu);
                else
                {
                    Block.Reset(obj.Name, jmnu);
                }
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void excuteSingle(Any obj,ExcutingStack stack)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void excuteBinary(Any obj1, Any obj2,ExcutingStack stack)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Excute(ExcutingStack stack)
        {
             stack.Push(this, Block.GetVariable(ObjectName));
        }

     
    }
    public class CallNode : OperatorNode
    {
        public CallNode(Block block)
        {
            Block = block;
        }
        public Block Block { get; set; }
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.Call;

        public override void DoCheck()
        {
            
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Excute(ExcutingStack stack)
        {
            Operands[0].Excute(stack);

            var func = stack.Get(Operands[0]);

           if(func is JFunction jsf)
            {
               

                var ls = new List<Any>();

                for (int i = 1; i< Operands.Count; i++)
                {
                    Operands[i].Excute(stack);

                    ls.Add(stack.Get(Operands[i]));
                }


                stack.Push(this, jsf.Excute(ls.ToArray()));

            }
           else if(func is JMappingFunction jmaf)
            {
                var ls = new List<object>();

                for (int i = 1; i < Operands.Count; i++)
                {
                    Operands[i].Excute(stack);
                    ls.Add(stack.Get(Operands[i]).GetObject());
                }

                var result = jmaf.Invoker(jmaf.Parent.Instance, ls.ToArray());

                var type = result.GetType();

                if (BaseTypes.Numbers.Contains(type))
                {
                    stack.Push(this, JNumber.CreateJNumber(result));
                }
                else if(BaseTypes.TString==type)
                {
                   stack.Push(this,new JString((string)result));
                }
                else if(BaseTypes.TBoolean==type)
                {
                    stack.Push(this,new JBool((bool)result));
                }
                else if(type==typeof(void))
                {
                    stack.Push(this, new JVoid());
                }
                else
                {
                    stack.Push(this, new JMappingObject(result));
                }
            }
           else
            {
                throw new MemberNotAFunctionException();
            }



        }
    }

    public class FunctionDefineNode : OperatorNode
    {
        public FunctionDefineNode(Block block)
        {
            Block = block;
        }
        public Block Block { get; set; }
        public override OutputType OutputType => OutputType.Object;

        public override OperatorType OperatorType => OperatorType.Function;

        public override void DoCheck()
        {
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Excute(ExcutingStack stack)
        {
            Block.Declare(((JFunction)((OperandNode)Operands[0]).Output).Name, ((OperandNode)Operands[0]).Output);
        }
    }

    public class BreakNode : OperatorNode
    {
        public BreakNode(BreakableBlock block)
        {
            Block = block;
        }
        public override OutputType OutputType => OutputType.None;
        public BreakableBlock Block { get; set; }

        public override OperatorType OperatorType => OperatorType.Break;

        public override void DoCheck()
        {
          
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Excute(ExcutingStack stack)
        {
            Block.Break();
        }
    }

    public class ContinueNode:BreakNode
    {
        public ContinueNode(BreakableBlock block) : base(block)
        {
        }

        public override OperatorType OperatorType => OperatorType.Continue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Excute(ExcutingStack stack)
        {
            Block.Continue();
        }
    }

    public class ReturnOperatorNode : SingleOperatorNode
    {
        public ReturnOperatorNode(Block block)
        {
            Block = block;
        }
        public Block Block { get; set; }
        public override OperatorType OperatorType =>OperatorType.Return;

        public override OutputType OutputType => OutputType.Object;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void DoCheck()
        {
        }

        protected override void excuteSingle(Any obj,ExcutingStack stack)
        {
            ((BreakableBlock)Block).Return(obj);
        }
    }

    public class TernaryOperatorNode : OperatorNode
    {
        public override OperatorType OperatorType => throw new System.NotImplementedException();

        public override OutputType OutputType => throw new System.NotImplementedException();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void DoCheck()
        {
        }

        public override void Excute(ExcutingStack stack)
        {
            Operands[0].Excute(stack);


            if ((bool)stack.Get(Operands[0]))
            {
                Operands[1].Excute(stack);
                stack.Push(this, stack.Get(Operands[1]));
            }
            else
            {
                Operands[2].Excute(stack);
                stack.Push(this, stack.Get(Operands[2]));
            }

        }
    }
}
