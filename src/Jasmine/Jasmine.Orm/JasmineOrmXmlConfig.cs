using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Implements;
using Jasmine.Reflection;
using System;
using System.Xml;

namespace Jasmine.Orm
{
    public class JasmineOrmXmlConfig
    {
        public static void LoadConfig(string path)
        {
            var xml = new XmlDocument();

            xml.Load(path);

            foreach (var item in xml.GetElementsByTagName("jasmine-orm-config"))
            {
                foreach (var item1 in ((XmlNode)item).ChildNodes)
                {
                    var node = item1 as XmlNode;

                    if (node.Name == "type-handler")
                    {
                        var type = node.Attributes["type"].Value;
                        var handlerType = node.Attributes["handler-type"].Value;

                        var handlerInstance = (ISqlConvertor)(DefaultReflectionCache.Instance.GetItem(Type.GetType(handlerType)).Constructors.GetDefaultConstructor().Invoke());

                        ((DefaultSqlConvertorProvider)DefaultSqlConvertorProvider.Instance).AddConvertor(Type.GetType(type), handlerInstance);

                    }
                    else if (node.Name == "template")
                    {
                        DefaultTemplateProvider.Instance.SaveTemplate(node.InnerXml, node.Attributes["name"].Value);
                    }

                }
            }




        }
    }
}
