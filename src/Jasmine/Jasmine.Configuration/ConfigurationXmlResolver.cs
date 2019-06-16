using Jasmine.Configuration.Exceptions;
using Jasmine.Extensions;
using System.Collections.Generic;
using System.Xml;

namespace Jasmine.Configuration
{
    public class ConfigurationXmlResolver
    {
        private ConfigurationXmlResolver()
        {

        }


        private const string CONFIG_GROUP = "config-group";
        private const string NAME = "name";
        private const string VALUE = "value";
        private const string IMPORT = "import";
        private const string PATH = "path";

        private readonly HashSet<string> _loadedPathes = new HashSet<string>();

        public static readonly ConfigurationXmlResolver Instance = new ConfigurationXmlResolver();

        public ConfigrationGroup[] Resolve(string path)
        {
            var ls = new List<ConfigrationGroup>();

            var xml = new XmlDocument();

            _loadedPathes.Add(path);

            xml.Load(path);

            foreach (var groupNode in xml.GetAll(x=>x.Name==CONFIG_GROUP))
            {

                var name = groupNode.GetAttribute(NAME);

                requireAttributeExists(name, CONFIG_GROUP, NAME);

                var group = new ConfigrationGroup(name);

                foreach (var property in groupNode)
                {
                    var propertyNode = property as XmlNode;

                    if (propertyNode.Name == IMPORT)
                    {
                        var outterPath = propertyNode.GetAttribute(PATH);

                        requireAttributeExists(name, IMPORT, PATH);

                        if (!_loadedPathes.Contains(outterPath))
                            ls.AddRange(Resolve(outterPath));
                    }
                    else
                    {
                        resolveProperty(group, "", propertyNode);
                    }
                }

                ls.Add(group);
            }

            return ls.ToArray();

        }

        private void requireAttributeExists(string value, string tag, string attribute)
        {
            if (value == null)
                throw new RequiredTagAttributeNotFoundException($" tag \"{tag}\"'s attribute \"{attribute}\" is required! ");
        }

        private void resolveProperty(ConfigrationGroup group, string prefix, XmlNode node)
        {

            var property = new Property();

            property.Name = node.Name.ToLower();

            var value = node.GetAttribute(VALUE);

            if (value == null)
                value = node.InnerText;

            property.Segments = new PropertyParser().Parse(value);

            group.AddProperty(property);

            //do recursive
            foreach (var subNode in node.ChildNodes)
            {
                var propertyNode = subNode as XmlNode;

                resolveProperty(group, property.Name + ".", propertyNode);
            }

        }


    }
}
