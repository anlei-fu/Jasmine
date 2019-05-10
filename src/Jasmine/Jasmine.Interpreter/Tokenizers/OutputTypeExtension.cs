using Jasmine.Interpreter.AstTree;

namespace Jasmine.Interpreter.Tokenizers
{
    public static  class OutputTypeExtension
    {
        public static bool IsBool(this OutputType type)
        {
            return type == OutputType.Bool || type == OutputType.Object;
        }
        public static bool IsNumber(this OutputType type)
        {
            return type == OutputType.Number || type == OutputType.Object;
        }
        public static bool IsString(this OutputType type)
        {
            return type == OutputType.Object || type == OutputType.String;
        }
        public static bool IsBoolStrict(this OutputType type)
        {
            return type == OutputType.Bool;
        }
        public static bool IsNumberStrict(this OutputType type)
        {
            return type == OutputType.Number;
        }
        public static bool IsStringStrict(this OutputType type)
        {
            return  type == OutputType.String;
        }
    }
}
