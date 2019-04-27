﻿using Jasmine.Spider.Grammer;
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
    }
}