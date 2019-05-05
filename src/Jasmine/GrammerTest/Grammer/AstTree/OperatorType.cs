namespace Jasmine.Spider.Grammer
{
    public enum OperatorType
    {

        /// <summary>
        /// =
        /// </summary>
        Assignment,
        /// <summary>
        /// +=
        /// </summary>
        AddAsignment,
        /// <summary>
        /// -=
        /// </summary>
        SubtractAsignment,
        /// <summary>
        /// *=
        /// </summary>
        MutiplyAsignment,
        /// <summary>
        /// /=
        /// </summary>
        DevideAsignment,
        /// <summary>
        /// %=
        /// </summary>
        ModAsignment,
        /// <summary>
        /// &&
        /// </summary>
        And,
        /// <summary>
        /// ||
        /// </summary>
        Or,
        /// <summary>
        /// !
        /// </summary>
        Not,
        /// <summary>
        /// ==
        /// </summary>
        Equel,
        /// <summary>
        /// .
        /// </summary>
        MemberAccess,
        /// <summary>
        /// !=
        /// </summary>
        NotEquel,
        /// <summary>
        /// (
        /// </summary>
        LeftParenthesis,
        /// <summary>
        /// )
        /// </summary>
        RightParenthesis,
        /// <summary>
        /// {
        /// </summary>
        LeftBrace,
        /// <summary>
        /// }
        /// </summary>
        RightBrace,
        /// <summary>
        /// [
        /// </summary>
        LeftSquare,
        /// <summary>
        /// ]
        /// </summary>
        RightSquare,
        /// <summary>
        /// +
        /// </summary>
        Add,
        /// <summary>
        /// -
        /// </summary>
        Subtract,
        /// <summary>
        /// %
        /// </summary>
        Mod,
        /// <summary>
        /// *
        /// </summary>
        Mutiply,
        /// <summary>
        /// /
        /// </summary>
        Devide,
        /// <summary>
        /// ++ only left increment surpported
        /// </summary>
        Increment,
        /// <summary>
        /// -- only left decrement surpported
        /// </summary>
        Decrement,
        /// <summary>
        /// ?
        /// </summary>
        Ternary,
        /// <summary>
        /// :
        /// </summary>
        Binary,
        /// <summary>
        /// ,
        /// </summary>
        Coma,
        /// <summary>
        /// ;
        /// </summary>
        ExpressionEnd,
        /// <summary>
        /// >
        /// </summary>
        Bigger,
        /// <summary>
        /// >=
        /// </summary>
        BiggerEquel,
        /// <summary>
        /// <
        /// </summary>
        Less,
        /// <summary>
        /// <=
        /// </summary>
        LessEquel,
    
        /// <summary>
        /// new
        /// </summary>
        NewInstance,
        /// <summary>
        /// var
        /// </summary>
        Declare,
        /// <summary>
        /// function
        /// </summary>
        Function,
        /// <summary>
        /// break
        /// </summary>
        Break,
        /// <summary>
        /// continue
        /// </summary>
        Continue,
        /// <summary>
        /// return
        /// </summary>
        Return,
         /// <summary>
        /// 
        /// 
        /// /// </summary>
        Call,
        /// <summary>
        /// 
        /// </summary>
        QueryObject,
        /// <summary>
        /// -
        /// </summary>
        Minus,
        Operand,
        ArrayIndex,
        ObjectMember


    }
}
