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
                    break;
                case OperatorType.And:
                    break;
                case OperatorType.Or:
                    break;
                case OperatorType.Not:
                    break;
                case OperatorType.Equel:
                    break;
                case OperatorType.Member:
                    break;
                case OperatorType.NotEquel:
                    break;
                case OperatorType.Call:
                    break;
                case OperatorType.LeftParenn:
                    break;
                case OperatorType.RightParenn:
                    break;
                case OperatorType.LeftBrace:
                    break;
                case OperatorType.RightBrace:
                    break;
                case OperatorType.LeftSquare:
                    break;
                case OperatorType.RightSquare:
                    break;
                case OperatorType.Add:
                    break;
                case OperatorType.Reduce:
                    break;
                case OperatorType.Mode:
                    break;
                case OperatorType.Mutiply:
                    break;
                case OperatorType.Devide:
                    break;
                case OperatorType.LeftIncrement:
                    break;
                case OperatorType.RightIncrement:
                    break;
                case OperatorType.LeftDecrement:
                    break;
                case OperatorType.RightDecrement:
                    break;
                case OperatorType.Coma:
                    break;
                case OperatorType.ExpressionEnd:
                    break;
                case OperatorType.Bigger:
                    break;
                case OperatorType.BiggerEquel:
                    break;
                case OperatorType.Less:
                    break;
                case OperatorType.LessEquel:
                    break;
                case OperatorType.QueryObJect:
                    break;
                case OperatorType.New:
                    break;
                case OperatorType.Var:
                    break;
                case OperatorType.Break:
                    break;
                case OperatorType.Continue:
                    break;
                default:
                    break;
            }
        }
        public static OperatorOprandType GetOperandType(this OperatorType type)
        {
            switch (type)
            {
                case OperatorType.Bigger:
                case OperatorType.BiggerEquel:
                case OperatorType.Less:
                case OperatorType.LessEquel:

                case OperatorType.Assignment:
                case OperatorType.Not:
                case OperatorType.And:
                case OperatorType.Or:
                case OperatorType.Equel:
                case OperatorType.NotEquel:


                case OperatorType.Mutiply:
                case OperatorType.Devide:
                case OperatorType.Mode:
                case OperatorType.Add:
                case OperatorType.Reduce:
                    return OperatorOprandType.Binary;

                case OperatorType.LeftIncrement:
                case OperatorType.RightIncrement:
                case OperatorType.LeftDecrement:
                case OperatorType.RightDecrement:
                case OperatorType.Var:
                case OperatorType.Member:
                    return OperatorOprandType.Single;

                case OperatorType.Call:
                    return OperatorOprandType.Mutiple;



                case OperatorType.LeftParenn:
                    break;
                case OperatorType.RightParenn:
                    break;
                case OperatorType.LeftBrace:
                    break;
                case OperatorType.RightBrace:
                    break;
                case OperatorType.LeftSquare:
                    break;
                case OperatorType.RightSquare:
                    break;


                case OperatorType.Coma:
                    break;
                case OperatorType.QueryObJect:
                    break;


                case OperatorType.ExpressionEnd:
                case OperatorType.New:

                    break;


                case OperatorType.Break:
                    break;
                case OperatorType.Continue:
                    break;
                default:
                    break;
            }
        }

        public static OperatorResultType GetResultType(this OperatorType op)
        {
            switch (op)
            {
                case OperatorType.Bigger:
                case OperatorType.BiggerEquel:
                case OperatorType.Less:
                case OperatorType.LessEquel:
                case OperatorType.And:
                case OperatorType.Or:
                case OperatorType.Not:
                case OperatorType.Equel:
                case OperatorType.NotEquel:
                    return OperatorResultType.Bool;

                case OperatorType.QueryObJect:
                case OperatorType.New:
                case OperatorType.Var:
                case OperatorType.Member:
                case OperatorType.Call:
                    return OperatorResultType.Variable;

                case OperatorType.Add:
                case OperatorType.Reduce:
                case OperatorType.Mode:
                case OperatorType.Mutiply:
                case OperatorType.Devide:
                case OperatorType.LeftIncrement:
                case OperatorType.RightIncrement:
                case OperatorType.LeftDecrement:
                case OperatorType.RightDecrement:
                    return OperatorResultType.Number;

                case OperatorType.Assignment:
                case OperatorType.LeftParenn:
                case OperatorType.RightParenn:
                case OperatorType.LeftBrace:
                case OperatorType.RightBrace:
                case OperatorType.LeftSquare:
                case OperatorType.RightSquare:
                case OperatorType.Coma:
                case OperatorType.ExpressionEnd:
                case OperatorType.Break:
                case OperatorType.Continue:
                default:
                    return OperatorResultType.None;
            }
        }


        private static Dictionary<OperatorType, OperatorConstraint> _dic = new Dictionary<OperatorType, OperatorConstraint>()
        {
            {OperatorType.Assignment,OperatorConstraint.Varible},
            {OperatorType.And,OperatorConstraint.Bool},
            {OperatorType.Or,OperatorConstraint.Bool},
            {OperatorType.Not,OperatorConstraint.Bool},
            {OperatorType.Equel,OperatorConstraint.Varible},
            {OperatorType.NotEquel,OperatorConstraint.Varible},
            {OperatorType.Bigger,OperatorConstraint.Number},
            {OperatorType.BiggerEquel,OperatorConstraint.Number},
            { OperatorType.Less,OperatorConstraint.Number},
            {OperatorType.LessEquel,OperatorConstraint.Number},
            {OperatorType.Add,OperatorConstraint.Number},
            {OperatorType.Reduce,OperatorConstraint.Number},
            {OperatorType.Mutiply,OperatorConstraint.Number},
            {OperatorType.Devide,OperatorConstraint.Number},
            {OperatorType.Mode,OperatorConstraint.Number},
            {OperatorType.Var,OperatorConstraint.String},
            {OperatorType.New,OperatorConstraint.String},
            {OperatorType.Member,OperatorConstraint.String},

        };
        public static  OperatorConstraint GetInpuConstraint(this OperatorType op)
        { 
            
        }
        
    }
}
