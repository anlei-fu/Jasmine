using Jasmine.Spider.Grammer;
using System.Collections.Generic;

namespace GrammerTest.Grammer
{
    public static class OperatorExtension
    {

        public static int GetPriority(this OperatorType type)
        {
            switch (type)
            {
                case OperatorType.Assignment:
                case OperatorType.AddAsignment:
                case OperatorType.ReduceAsignment:
                case OperatorType.MutiplyAsignment:
                case OperatorType.DevideAsignment:
                case OperatorType.ModAsignment:
                    return 0;
                case OperatorType.New:
                case OperatorType.Var:
                case OperatorType.Function:
                    return 1;
                case OperatorType.And:
                case OperatorType.Or:
                    return 2;
                case OperatorType.Not:
                    return 3;
                case OperatorType.Bigger:
                case OperatorType.BiggerEquel:
                case OperatorType.Less:
                case OperatorType.LessEquel:
                case OperatorType.NotEquel:
                case OperatorType.Equel:
                    return 4;
              
             
                case OperatorType.Add:
                case OperatorType.Reduce:
                    return 5;
                case OperatorType.Mod:
                case OperatorType.Mutiply:
                case OperatorType.Devide:
                    return 6;
                case OperatorType.Increment:
                case OperatorType.Decrement:
                    return 7;
                case OperatorType.Member:
                    return 8;
                //never mind
                case OperatorType.Break:
                case OperatorType.Continue:
                case OperatorType.Question:
                case OperatorType.Semicolon:
                case OperatorType.Coma:
                case OperatorType.ExpressionEnd:
                case OperatorType.LeftParenthesis:
                case OperatorType.RightParenthesis:
                case OperatorType.LeftBrace:
                case OperatorType.RightBrace:
                case OperatorType.LeftSquare:
                case OperatorType.RightSquare:
                default:
                    return -1;
            }
        }

        public static bool IsBinaryOperator(this OperatorType type) => !(type.IsUnaryOperator() || type.IsMutipleOperator());

        public static bool IsMutipleOperator(this OperatorType type) => type == OperatorType.Call;
    
        public static bool IsUnaryOperator(this OperatorType type)
        {
            switch (type)
            {

                case OperatorType.Var:
                case OperatorType.Not:
                case OperatorType.Increment:
                case OperatorType.Decrement:
                    return true;
                default:
                    return false;
            }
        }

 
        
        public static bool CanBeStartWithNoOperand(this OperatorType op)
        {
            return true;
        }

        public static bool ComparePriority(this OperatorType op,OperatorType other)
        {
            return other.GetPriority()==op.GetPriority();
        }

        public static string Tostring0(this OperatorType type)
        {
            switch (type)
            {
                case OperatorType.Assignment:
                    return "=";
                case OperatorType.AddAsignment:
                   return "+=";
                case OperatorType.ReduceAsignment:
                   return "-=";
                case OperatorType.MutiplyAsignment:
                   return "*=";
                case OperatorType.DevideAsignment:
                   return "/=";
                case OperatorType.ModAsignment:
                   return "%=";
                case OperatorType.And:
                   return "&&";
                case OperatorType.Or:
                   return "||";
                case OperatorType.Not:
                   return "!";
                case OperatorType.Equel:
                   return "==";
                case OperatorType.Member:
                   return ".";
                case OperatorType.NotEquel:
                   return "!=";
                case OperatorType.LeftParenthesis:
                   return "(";
                case OperatorType.RightParenthesis:
                   return ")";
                case OperatorType.LeftBrace:
                   return "{";
                case OperatorType.RightBrace:
                   return "}";
                case OperatorType.LeftSquare:
                   return "[";
                case OperatorType.RightSquare:
                   return "]";
                case OperatorType.Add:
                   return "+";
                case OperatorType.Reduce:
                   return "-";
                case OperatorType.Mod:
                   return "%";
                case OperatorType.Mutiply:
                   return "*";
                case OperatorType.Devide:
                   return "/";
                case OperatorType.Increment:
                   return "++";
                case OperatorType.Decrement:
                   return "--";
                case OperatorType.Question:
                   return "?";
                case OperatorType.Semicolon:
                   return ":";
                case OperatorType.Coma:
                   return ",";
                case OperatorType.ExpressionEnd:
                   return ";";
                case OperatorType.Bigger:
                   return ">";
                case OperatorType.BiggerEquel:
                   return ">=";
                case OperatorType.Less:
                   return "<";
                case OperatorType.LessEquel:
                   return "<=";
                case OperatorType.New:
                   return "new";
                case OperatorType.Var:
                   return "var";
                case OperatorType.Function:
                   return "function";
                case OperatorType.Break:
                   return "break";
                case OperatorType.Continue:
                   return "continue";
                case OperatorType.Call:
                   return "call";
                case OperatorType.QueryScope:
                   return "";
                case OperatorType.Minus:
                   return "-";
                default:
                    return string.Empty;
            }
        }

    }
}
