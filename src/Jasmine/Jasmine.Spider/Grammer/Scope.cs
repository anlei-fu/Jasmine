using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public abstract class Scope:Excutor
    {
       public IDictionary<string, object> Varibles { get; set; }
       public Scope Parent { get; set; }
       public IList<Excutor> Children { get; set; }
       public void Clear()
       {
            Varibles.Clear();
       }


    }
    
}
