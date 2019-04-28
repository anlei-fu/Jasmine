namespace GrammerTest.Grammer.Scopes
{
    public abstract class Block : AbstractExcutor
    {
        public override string Name => ".Block";
        public IVariableTable VariableTable { get; set; }

    }
}
