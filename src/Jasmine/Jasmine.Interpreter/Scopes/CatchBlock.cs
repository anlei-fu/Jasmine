namespace Jasmine.Interpreter.Scopes
{
    public class CatchBlock : BodyBlock
    {
        public CatchBlock(BreakableBlock parent) : base(parent)
        {
        }

        public string ErrorName { get; internal set; }
       

       

       
    }
}
