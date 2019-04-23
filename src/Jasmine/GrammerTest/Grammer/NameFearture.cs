using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public   class NameFearture
    {
        public NameFearture(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidNameException($" inavalid name ! the name must be not null nor empty! ");

            Name = name;
        }
        public string Name { get; }
    }
  
}
