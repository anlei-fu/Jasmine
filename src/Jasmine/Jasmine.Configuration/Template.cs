namespace Jasmine.Configuration
{
    public class Template
    {
        public string Value { get; set; }
        public bool IsVariable { get; set; }

        public bool IsPropertyNode => PropertyNode!=null;
        public PropertyNode PropertyNode { get; set; }

      
    }
}
