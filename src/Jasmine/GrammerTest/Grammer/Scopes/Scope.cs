using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public abstract class Scope:IExcutor
    {
       private IDictionary<string, JObject> _loacals { get; set; }
       public Scope Parent { get; set; }
       public IList<IExcutor> Children { get; set; }
       
       public void Clear()
       {
            _loacals.Clear();
       }

        public JObject GetVariable(string name)
        {
            return null;
        }

       
        public JObject Declare(string name)
        {
            var obj = new JObject();
         

            Declare(name, obj);

            return obj;
        }

        public JObject Declare(string name, JObject obj )
        {
            if (_loacals.ContainsKey(name))
            {
                // a variable can define only one time 
            }

            _loacals.Add(name, obj);

            return null;
        }

        public abstract void Excute();

      

    }
    
}
