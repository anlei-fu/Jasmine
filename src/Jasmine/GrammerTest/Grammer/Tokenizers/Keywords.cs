using System.Collections.Generic;

namespace GrammerTest.Grammer
{
    public class Keywords
    {
        /*
         *Using 
         *
         */
        public const string FUNCTION = "function";
        public const string FOR = "for";
        public const string FOREACH = "foreach";
        public const string DO = "do";
        public const string WHILE = "while";
        public const string BREAK = "break";
        public const string CONTINUE = "continue";
        public const string VAR = "var";
        public const string IF = "if";
        public const string ELSE = "else";
        public const string ELIF = "elif";
        public const string TRY = "try";
        public const string CATCH = "catch";
        public const string FINALLY = "finally";
        public const string IN = "in";
        public const string RETURN = "return";
        public const string NEW = "new";
        public const string THROW = "throw";


        /*
         * Reserved keywords
         * 
         */
        public const string CLASS = "";
        public const string NAMESPACE = "";
        public const string PUBLIC = "";
        public const string PRIVATE = "";
        public const string INTERNAL = "";
        public const string SWITCH = "";
        public const string CASE = "";
        public const string AWAIT = "";
        public const string SYCHRINIZED = "";
        public const string ASYNC = "";
        public const string OUT = "";
        public const string LET = "";
        public const string PACKET = "";
        public const string SUPER = "";
        public const string FINAL = "";
        public const string USING = "";
        public const string DEFAULT = "";
        public const string WITH = "";
        public const string INPLEMENTS = "";
        public const string EXTENDS = "";
        public const string STRUCT = "";
        public const string VOID = "";
        public const string SEALED = "";
        public const string IS = "";
        public const string AS = "";
        public const string INSTANCEOF = "";
        public const string TYPEOF = "";
        public const string NAMEOF = "";
        public const string LOCK = "";

        public static readonly HashSet<string> KeyWordsUsing = new HashSet<string>
        {

        };
        public static readonly HashSet<string> KeyWordsReserved = new HashSet<string>()
        {

        };
        public static readonly HashSet<string> AllKeywords = new HashSet<string>()
        {

        };


    }
}
