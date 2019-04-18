using Jasmine.Common.Exceptions;
using System.IO;
using System.Xml;

namespace Jasmine.Common
{
    public class JasminePropertyManagerXmlLoader
    {
        private const string JASMINE_MANAGER_TAG = "jasmine-property-manager";
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

            foreach (var item in xd.GetElementsByTagName(JASMINE_MANAGER_TAG))
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

                    manager.SubManagers.Add(subManager.Name, subManager);
                }
                else if (subNode.Name == PROPERTY)//property
                {
                    if (subNode.Attributes.GetNamedItem(NAME) == null)//name requird
                        throw new JasminePropertyException("property-tag's name property is required!");

                    var key = subNode.Attributes[NAME].Value;
                    var value = string.Empty;

                    if (subNode.Attributes.GetNamedItem(VALUE) == null)//load value from subchild innertext,for big value content
                    {
                        if (subNode.FirstChild.Name != VALUE)
                            throw new JasminePropertyException("property-tag'first child must be <value></value> tag!");

                        value = subNode.FirstChild.InnerText;
                    }
                    else
                    {
                        if (subNode.Attributes.GetNamedItem(VALUE) == null)
                            throw new JasminePropertyException("property's value attribute is required,or first child is tag <value></value>");

                        value = subNode.Attributes[VALUE].Value;
                    }

                    manager.Properties.Add(key.Trim(), value.Trim());

                }
                else if (subNode.Name == JASMINE_MANAGER_TAG)//load sub manager
                {
                    var subManager = load(subNode);

                    manager.SubManagers.Add(subManager.Name, subManager);
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
            var xml = new XmlDocument();
            var node = xml.CreateElement(JASMINE_MANAGER_TAG);
            var attr = xml.CreateAttribute(NAME);
            attr.Value = manager.Name;
            node.Attributes.Append(attr);
            node.AppendChild(node);

            foreach (var item in manager.Properties)
            {
                var property = xml.CreateElement(PROPERTY);
                var property_name = xml.CreateAttribute(NAME);
                property_name.Value = item.Key;

                var value = xml.CreateElement(VALUE);
                value.InnerText = item.Value;

                property.AppendChild(value);

                node.AppendChild(property);
            }

            foreach (var item in manager.SubManagers)
            {
                node.AppendChild(getNode(item.Value));
            }



            return node;

        }
    }
}
