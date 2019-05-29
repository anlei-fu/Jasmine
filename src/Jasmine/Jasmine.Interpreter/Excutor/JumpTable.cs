using System.Collections.Generic;

namespace Jasmine.Interpreter.Excutor
{
    public  class JumpTable
    {

        public LocalVaribleTable LocalVaribleTable { get; set; }
        public OperandStack OperandStack { get; set; }

        public List<JumpPoint> JumpPoints { get; set; }

        public int CrrentIndex { get; set; }

        public JumpPoint Pop()
        {
            return null;
        }

        

        public void Push(JumpPoint point)
        {

        }
        public void JumpBreak()
        {

        }
        public void JumpContinue()
        {

        }
        public void JumpBlockEnd()
        {

        }

        public void JumpReturn()
        {

        }
    }

    public class JumpPoint
    {
        public JumpPointType JumpPointType { get; set; }
        public bool PopLocalVarible { get; set; }
        public int OperandStackIndex { get; set; }
    }

    public enum JumpPointType
    {
        Blocked,
        LoopEnd,
        IfEnd,
        Return,
    }
}
