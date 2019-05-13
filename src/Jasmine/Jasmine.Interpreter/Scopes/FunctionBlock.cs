using Jasmine.Interpreter.TypeSystem;

namespace Jasmine.Interpreter.Scopes
{
    public class FunctionBlock : BodyBlock
    {
        public const string RETURN = "__RETURN__";
        public FunctionBlock(BreakableBlock parent) : base(parent)
        {
        }

        public override void Break()
        {
            throw new System.NotImplementedException();
        }

        public override void Catch(JError error)
        {
            throw new System.NotImplementedException();
        }

        public override void Continue()
        {
            throw new System.NotImplementedException();
        }

        public override void Excute(ExcutingStack stack)
        {
            Declare(RETURN, new JObject());

            var newStack = ExcutingStackPool.Instance.Rent();

            Body.Excute(stack);

            ExcutingStackPool.Instance.Recycle(newStack);
        }

        public override void Return(Any result)
        {
            Reset(RETURN, result);
        }
    }
}
