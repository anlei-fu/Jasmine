using Jasmine.Interpreter.TypeSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Interpreter.Excutor
{
  public  class StackFrame
    {
        public InstructGroup InstructStack { get; set; }
        public JumpTable JumpTable { get; set; }
        public OperandStack OperandStack { get; set; }
        public LocalVaribleStack LocalVaribleStack { get; set; }
        public StackFrame ReturnFrame { get; set; }
        public int ReturnIndex { get; set; }
        public Any ReturnValue { get; set; }

        public void Excute()
        {
            for (int i = 0; i < InstructStack.Instructs.Length; i++)
            {
                switch (InstructStack.Instructs[i].OperatorType)
                {
                    case Tokenizers.OperatorType.Assignment:
                        break;
                    case Tokenizers.OperatorType.AddAsignment:
                        break;
                    case Tokenizers.OperatorType.SubtractAsignment:
                        break;
                    case Tokenizers.OperatorType.MutiplyAsignment:
                        break;
                    case Tokenizers.OperatorType.DevideAsignment:
                        break;
                    case Tokenizers.OperatorType.ModAsignment:
                        break;
                    case Tokenizers.OperatorType.And:
                        break;
                    case Tokenizers.OperatorType.Or:
                        break;
                    case Tokenizers.OperatorType.Not:
                        break;
                    case Tokenizers.OperatorType.Equel:
                        break;
                    case Tokenizers.OperatorType.MemberAccess:
                        break;
                    case Tokenizers.OperatorType.NotEquel:
                        break;
                    case Tokenizers.OperatorType.LeftParenthesis:
                        break;
                    case Tokenizers.OperatorType.RightParenthesis:
                        break;
                    case Tokenizers.OperatorType.LeftBrace:
                        break;
                    case Tokenizers.OperatorType.RightBrace:
                        break;
                    case Tokenizers.OperatorType.LeftSquare:
                        break;
                    case Tokenizers.OperatorType.RightSquare:
                        break;
                    case Tokenizers.OperatorType.Add:
                        break;
                    case Tokenizers.OperatorType.Subtract:
                        break;
                    case Tokenizers.OperatorType.Mod:
                        break;
                    case Tokenizers.OperatorType.Mutiply:
                        break;
                    case Tokenizers.OperatorType.Devide:
                        break;
                    case Tokenizers.OperatorType.Increment:
                        break;
                    case Tokenizers.OperatorType.Decrement:
                        break;
                    case Tokenizers.OperatorType.Ternary:
                        break;
                    case Tokenizers.OperatorType.Binary:
                        break;
                    case Tokenizers.OperatorType.Coma:
                        break;
                    case Tokenizers.OperatorType.ExpressionEnd:
                        break;
                    case Tokenizers.OperatorType.Bigger:
                        break;
                    case Tokenizers.OperatorType.BiggerEquel:
                        break;
                    case Tokenizers.OperatorType.Less:
                        break;
                    case Tokenizers.OperatorType.LessEquel:
                        break;
                    case Tokenizers.OperatorType.NewInstance:
                        break;
                    case Tokenizers.OperatorType.Declare:
                        break;
                    case Tokenizers.OperatorType.Function:
                        break;
                    case Tokenizers.OperatorType.Break:
                        break;
                    case Tokenizers.OperatorType.Continue:
                        break;
                    case Tokenizers.OperatorType.Return:
                        break;
                    case Tokenizers.OperatorType.Call:
                        break;
                    case Tokenizers.OperatorType.QueryObject:
                        break;
                    case Tokenizers.OperatorType.Minus:
                        break;
                    case Tokenizers.OperatorType.Operand:
                        break;
                    case Tokenizers.OperatorType.ArrayIndex:
                        break;
                    case Tokenizers.OperatorType.ObjectMember:
                        break;
                    case Tokenizers.OperatorType.BlockStart:
                        break;
                    case Tokenizers.OperatorType.BlockEnd:
                        break;
                    case Tokenizers.OperatorType.ForBlockEnd:
                        break;
                    case Tokenizers.OperatorType.ForBlockStart:
                        break;
                    case Tokenizers.OperatorType.Jump:
                        break;
                    case Tokenizers.OperatorType.JumpFalse:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
