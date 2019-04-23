using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public  class OperatorNode
    {
        public Scope Statement { get; set; }
        public OperandNode Output { get; set; }
        public IList<OperandNode> Operands { get; set; }
        public OperatorType Operator { get; set; }
    }
}
