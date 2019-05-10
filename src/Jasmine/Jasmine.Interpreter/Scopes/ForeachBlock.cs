using Jasmine.Interpreter.Scopes.Exceptions;
using Jasmine.Interpreter.TypeSystem;
using System.Collections;

namespace Jasmine.Interpreter.Scopes
{
    public class ForeachBlock : BodyBlock
    {
        public ForeachBlock(BreakableBlock parent) : base(parent)
        {
        }

        public Expression GetCollectionExpression { get; set; }
        public DeclareExpression DeclareExpression { get; set; }
        public IEnumerator Enumerabtor { get; set; }
        public string IteratorName { get; set; }

        private bool _break;
        public override void Break()
        {
            _break = true;
        }

        
     
        public override void Continue()
        {
           
        }

        public override void Excute()
        {

            var result = GetCollectionExpression.Root.Output;


            if (result is JMappingObject jma)
            {
                Enumerabtor = jma.Instance as IEnumerator;

                while (Enumerabtor.MoveNext())
                {
                    if (_break)
                        break;

                    Reset(IteratorName, new JMappingObject(Enumerabtor.Current));
                    Body.Excute();
                }
            }
            else if(result.Type ==JType.Object)
            {

            }
            else
            {
                throw new CanNotCastToEnumerableException();
            }


           

            
        }

        
    }
}
