using Jasmine.Parsers.Html;
using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public  class Extractor
    {
       public ExtractTarget Target { get; set; }
       public IDictionary<string,string> SplitorReplacer { get; set; }
       public string TargetName { get; set; }
        public string Output { get; set; }


       public ExtractOutput Extract(Element element)
        {
            return null;
        }
    }

    public enum ExtractTarget
    {
        InnerText,
        Attribute
    }
 
    public class ExtractOutput
    {
        public string ExtractedText { get; set; }
        public string RelatedTarget { get; set; }

    }
  
}
