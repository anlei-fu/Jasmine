using Jasmine.Interpreter.TypeSystem;

namespace Jasmine.Interpreter.Scopes
{
    public abstract class Instruct : IExcutor
    {
        public abstract string Name { get; }
        public abstract void Excute(Interpreter interpreter);
    }

    public class Jump:Instruct
    {
        public Jump(int position)
        {
            Position = position;
        }
        public int Position { get; }

        public override string Name => throw new System.NotImplementedException();

        public override void Excute(Interpreter interpreter)
        {
            throw new System.NotImplementedException();
        }
    }

    public class JumpUnary : Jump
    {
        public JumpUnary(int position) : base(position)
        {
        }
    }

    public class JumpBinary : Instruct
    {
        public int Position1 { get; }
        public int Position2 { get; }
        public override string Name => throw new System.NotImplementedException();

        public override void Excute(Interpreter interpreter)
        {
            throw new System.NotImplementedException();
        }
    }

    public abstract class BinaryLogic:Instruct
    {
        public override void Excute(Interpreter interpreter)
        {
            throw new System.NotImplementedException();
        }
        protected abstract JBool excuteInternal(JBool para1, JBool para2);
    }

    public class And : BinaryLogic
    {
        public override string Name => throw new System.NotImplementedException();

        protected override JBool excuteInternal(JBool para1, JBool para2)
        {
            throw new System.NotImplementedException();
        }
    }
    public class Or : BinaryLogic
    {
        public override string Name => throw new System.NotImplementedException();

        protected override JBool excuteInternal(JBool para1, JBool para2)
        {
            throw new System.NotImplementedException();
        }
    }

    public abstract class  BinaryNumber:Instruct
    {
        public override void Excute(Interpreter interpreter)
        {
            throw new System.NotImplementedException();
        }
        protected abstract JNumber excuteInternal(JNumber para1, JNumber para2);

    }

    public class Subtract : BinaryNumber
    {
        public override string Name => throw new System.NotImplementedException();

        protected override JNumber excuteInternal(JNumber para1, JNumber para2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Mutiply : BinaryNumber
    {
        public override string Name => throw new System.NotImplementedException();

        protected override JNumber excuteInternal(JNumber para1, JNumber para2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Devide : BinaryNumber
    {
        public override string Name => throw new System.NotImplementedException();

        protected override JNumber excuteInternal(JNumber para1, JNumber para2)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Mod : BinaryNumber
    {
        public override string Name => throw new System.NotImplementedException();

        protected override JNumber excuteInternal(JNumber para1, JNumber para2)
        {
            throw new System.NotImplementedException();
        }
    }







    public class InstructCollection
    {
        private Instruct[] _instructs;
        private bool hasNext => Current < Length;
        public int Current { get; private set; }
        public int Length => _instructs.Length;


        public void Excute(Interpreter interpreter)
        {
            while (hasNext)
            {
                Current++;
                _instructs[Current].Excute(interpreter);

            }
        }

       public void Jump(int position)
        {
            Current = position;
        }

    }
}
