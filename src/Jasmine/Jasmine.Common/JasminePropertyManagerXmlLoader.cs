using Jasmine.Common.Exceptions;
using Jasmine.Extensions;
using System.IO;
using System.Xml;

namespace Jasmine.Common
{
    public class JasminePropertyManagerXmlLoader
    {
        private const string PROPERTIES = "jasmine-property-manager";
        private const string NAME = "name";
        private const string IMPORT = "import";
        private const string REF = "ref";
        private const string PROPERTY = "property";
        private const string VALUE = "value";

        public static JasminePropertyManager Load(string path)
        {

            JasminePropertyManager manager=null;
            XmlDocument xd = new XmlDocument();

            xd.Load(path);

            foreach (var item in xd.GetElementsByTagName(PROPERTIES))
            {
                manager = load(item as XmlNode);
            }


            return manager;
        }
        private static JasminePropertyManager load(XmlNode node)
        {

            var manager = new JasminePropertyManager();

            if (node.Attributes.GetNamedItem(NAME) == null)// name attribute is required
                throw new JasminePropertyException("manager's name property is required!");

            manager.Name = node.Attributes[NAME].Value.Trim();

            foreach (var item in node.ChildNodes)
            {
                var subNode = item as XmlNode;

                if (subNode.Name == IMPORT)//import another file
                {
                    if (subNode.Attributes.GetNamedItem(REF) == null)
                        throw new JasminePropertyException("import-tag's ref property is required!");


                    var subManager = Load(subNode.Attributes[REF].Value);

                    foreach (var property in subManager)
                    {
                        manager.SetValue($"{subManager.Name}.{property.Key}", property.Value);
                    }
                   
                  
                }
                else if (subNode.Name == PROPERTIES)//property
                {

                    foreach (var property in subNode.GetDirectChildrenByTagName(PROPERTY))
                    {
                        if (property.Attributes.GetNamedItem(NAME) == null)//name requird
                            throw new JasminePropertyException("property-tag's name property is required!");

                        var key = subNode.Attributes[NAME].Value;
                        var value = string.Empty;

                        if (property.Attributes.GetNamedItem(VALUE) != null)//load value from subchild innertext,for big value content
                        {
                            value = subNode.FirstChild.InnerText;
                        }
                        else
                        {
                            if (property.FirstChild == null||property.FirstChild.Name!=VALUE)
                                throw new JasminePropertyException("value not found");

                            value = property.FirstChild.InnerText;
                        }

                        manager.SetValue($"{manager.Name}.{key}", value);
                      
                    }

                }
               

            }


            return manager;

        }
        public static void Write(JasminePropertyManager manager, string path)
        {
            File.WriteAllText(path, getNode(manager).ToString());
        }

        public static XmlNode getNode(JasminePropertyManager manager)
        {
            

            return null;

        }
    }
}
