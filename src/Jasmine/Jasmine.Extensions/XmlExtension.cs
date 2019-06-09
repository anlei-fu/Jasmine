using System;
using System.Collections.Generic;
using System.Xml;

namespace Jasmine.Extensions
{
    public  static class XmlExtensions
    {
        public static List<XmlNode> GetDirect(this XmlNode node)
        {
            return node.GetDirect(x => true);
        }

        public static List<XmlNode> GetAll(this XmlNode node)
        {
            return node.GetAll(x => true);
        }

        public static List<XmlNode> GetDirect(this XmlNode node,Predicate<XmlNode> matcher)
        {
            var ls = new List<XmlNode>();

            foreach (var item in node.ChildNodes)
            {
                var child = item as XmlNode;

                if (matcher(child))
                    ls.Add(child);

            }

            return ls;
        }
        public static List<XmlNode> GetAll(this XmlNode node,Predicate<XmlNode> matcher)
        {
            var ls = new List<XmlNode>();

            foreach (var item in node.ChildNodes)
            {
                var child = item as XmlNode;

                if (matcher(child))
                    ls.Add(child);

                if (child.ChildNodes != null)
                    ls.AddRange(child.GetAll(matcher));

            }

            return ls;
        }
        public static bool ContainsAnyInDirect(this XmlNode node,Predicate<XmlNode> matcher)
        {
            foreach (var item in node.ChildNodes)
            {
                var child = item as XmlNode;

                if (matcher(child))
                    return true;
            }

            return false;
        }

        public static bool ContiansAnyInAll(this XmlNode node, Predicate<XmlNode> matcher)
        {
            foreach (var item in node.ChildNodes)
            {
                var child = item as XmlNode;

                if (matcher(child)|| ContiansAnyInAll(child, matcher))
                    return true;
            }

            return false;
        }
        public static bool HasAttribute(this XmlNode node, string key)
        {

            return node == null || node.Attributes == null ?false
                                                           : node.Attributes.GetNamedItem(key)!=null;
        }
        public static string GetAttribute(this XmlNode node, string key)
        {

            return node == null || node.Attributes == null ? null
                                                           : node.Attributes.GetNamedItem(key)?.Value;
        }

        public static void SetAttribute(this XmlNode node, string key,string value)
        {
            if (node == null || node.Attributes == null)
                return;

            var item = node.Attributes.GetNamedItem(key);

            if (item == null)
                return ;

             item.Value=value;
        }

    }
}
