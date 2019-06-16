namespace Jasmine.Configuration
{
    public class PropertySegement
    {
        public string Value { get; set; }
        public bool IsVariable { get; set; }

        public bool IsTemplate => Template!=null;
        public PropertyTemplate Template { get; set; }

      
    }
}
