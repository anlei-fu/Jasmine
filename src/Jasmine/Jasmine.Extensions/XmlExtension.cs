using System;
using System.Collections.Generic;
using System.Xml;

namespace Jasmine.Extensions
{
    public  static class XmlExtension
    {

        public static List<XmlNode> GetDirectChildren(this XmlNode node,Predicate<XmlNode> matcher)
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
        public static List<XmlNode> GetAllChildren(this XmlNode node,Predicate<XmlNode>matcher)
        {
            var ls = new List<XmlNode>();

            foreach (var item in node.ChildNodes)
            {
                var child = item as XmlNode;

                if (matcher(child))
                    ls.Add(child);

                if (child.ChildNodes != null)
                    ls.AddRange(child.GetAllChildren(matcher));

            }

            return ls;
        }


        public static List<XmlNode> GetDirectChildrenByTagName(this XmlNode node,string name)
        {
       
            return node.GetDirectChildren(x => x.Name == name);
        }
        public static List<XmlNode> GetAllChildrenByTagName(this XmlNode node,string name)
        {
            return node.GetAllChildren(x => x.Name == name);
        }
        public static List<XmlNode>GetDirectChildrenByAttributeKey(this XmlNode node ,string key)
        {
            return node.GetDirectChildren(x => x.Attributes.GetNamedItem(key) != null);
        }

        public static List<XmlNode> GetAllChildrenByAttributeKey(this XmlNode node,string key)
        {
            return node.GetAllChildren(x => x.Attributes.GetNamedItem(key) != null);
        }
     
        public static List<XmlNode> GetDirectChildrenByAttributeKeyAndValue(this XmlNode node,string key,string value)
        {
            Predicate<XmlNode> matcher = child =>
              {
                  var attr = child.Attributes.GetNamedItem(key);

                  return attr != null && attr.Value == value;
              };

            return node.GetDirectChildren(matcher);
        }
        public static List<XmlNode>GetAllChildrenByAttributeKeyAndValue(this XmlNode node,string key,string value)
        {
            Predicate<XmlNode> matcher = child =>
            {
                var attr = child.Attributes.GetNamedItem(key);

                return attr != null && attr.Value == value;
            };

            return node.GetAllChildren(matcher);
        }
        public static List<XmlNode> GetDirectChildrenByTagNameAndAttributeKeyValue(this XmlNode node,string name,string key,string value)
        {
            Predicate<XmlNode> matcher = child =>
            {
                if (child.Name != name)
                    return false;

                var attr = child.Attributes.GetNamedItem(key);

                return attr != null && attr.Value == value;
            };

            return node.GetDirectChildren(matcher);

        }
        public static List<XmlNode> GetAllChildrenByTagNameAndAttributeKeyValue(this XmlNode node,string name,string key,string value)
        {
            Predicate<XmlNode> matcher = child =>
            {
                if (child.Name != name)
                    return false;

                var attr = child.Attributes.GetNamedItem(key);

                return attr != null && attr.Value == value;
            };

            return node.GetAllChildren(matcher);

        }

       
        public static bool ContainsDirectChildByTagName(this XmlNode node,string name)
        {
            return node.ContainsDirectChild(x => x.Name == name);
        }
        public static bool ContainsAllChildByTagName(this XmlNode node,string name)
        {
            return node.ContainsAllChild(x => x.Name == name);
        }
        public static bool ContainsDirectChildByAttributeKey(this XmlNode node,string key)
        {
            return node.ContainsDirectChild(x=>x.Attributes.GetNamedItem(key)!=null);
        }
        public static bool ContainsAllChildByAttributeKey(this XmlNode node,string key)
        {
            return node.ContainsAllChild(x=>x.Attributes.GetNamedItem(key)!=null);
        }
        public static bool ContainsDirectChildByAttributeKeyAndValue(this XmlNode node,string key,string value)
        {
            return true;
        }
        public static bool ContainsChildByAttributeKeyAndValue(this XmlNode node,string key,string value)
        {
            return true;
        }
        public static bool ContainsDirectChildByTagNameAndAttributeKeyValue(this XmlNode node,string name,string key ,string value)
        {
            return true;
        }
        public static bool ContainschildByTagNameAndAttributeKeyValue(this XmlNode node,string name ,string key,string value)
        {
            return true;
        }

        public static bool ContainsDirectChild(this XmlNode node,Predicate<XmlNode> matcher)
        {
            foreach (var item in node.ChildNodes)
            {
                var child = item as XmlNode;

                if (matcher(child))
                    return true;
            }

            return false;
        }
        public static bool ContainsAllChild(this XmlNode node, Predicate<XmlNode> matcher)
        {
            foreach (var item in node.ChildNodes)
            {
                var child = item as XmlNode;

                if (matcher(child))
                    return true;

                if (child.ChildNodes != null && child.ContainsDirectChild(matcher))
                    return true;

            }

            return false;
        }

        public static void RemoveDirectChildrenByAttributeKey(this XmlNode node,string key)
        {

        }
        public static void RemoveAllChildrenByAttributeKey(this XmlNode node,string key)
        {

        }

        public static void RemoveDirectChildrenByTagName(this XmlNode node,string name)
        {

        }
        public static void RemoveAllChildrenByTagName(this XmlNode node,string name)
        {

        }
        public static void RemoveDirectChildByAttributeKeyValue(this XmlNode node,string name,string key,string value)
        {

        }
        public static void RemoveAllChildrenByAttributeKeyValue(this XmlNode node,string key,string value)
        {

        }
        public static void RemoveDirectChildrenByTagNameAndAttributeKeyValue(this XmlNode node,string name,string key,string value)
        {

        }
        public static void RemoveAllChildrenByTagNameAndAttributeKeyValue(this XmlNode node,string name,string key,string value)
        {

        }
        public static void AppendAttribute(this XmlNode node,string newKey,string newValue)
        {

        }


        public static void AppendDirectChildrenAttributeByTagName(this XmlNode node,string name, string newKey,string newValue)
        {

        }
        public static void AppendAllChildrenAttributeByTagName(this XmlNode node,string name, string newKey,string NnewValue,bool overlay=false)
        {

        }
        public static void AppendDirenctChildByAttributeKey(this XmlNode node,string newKey,string newValue)
        {

        }
        public static void AppendAllChildrenByAttributeKey(this XmlNode node,string matchKey,string newKey,string newValue)
        {

        }
        public static void AppenDirectChildrenAttributeByKeyAndValue(this XmlNode node,string matchKey,string matchValue,string newKey,string newValue,bool overlay)
        {

        }
        public static void AppendDirectChildrenAttributeByTagNameAndKeyValue(this XmlNode node,string name,string matchKey,string matchValue,string newKey,string newValue,bool overlay)
        {

        }
        public static void AppenAllChildrenAttributeByTagNameAndAttributeKeyValue(this XmlNode node,string name,string matchKey,string matchValue,string newKey,string newValue,bool overlay)
        {

        }
        public static void RemoveDirectChildrenAttribute(this XmlNode node,string key)
        {

        }
        public static void RemoveDirectChildrenAttributeByTagName(this XmlNode node,string name,string key)
        {

        }
        public static void ClearAllChildrenAttributeByTagName(this XmlNode node,string name ,string key)
        {

        }
        public static void ClearAttribute(this XmlNode node)
        {

        }
        public static void ClearDirectChildrenAttributeByTag(this XmlNode node,string name,string key)
        {

        }
        public static List<XmlNode> GetDirectChildren(this XmlNode node)
        {
            var ls = new List<XmlNode>();

            foreach (var item in node.ChildNodes)
            {
                ls.Add((XmlNode)item);
            }

            return ls;
        }

      

        public static bool RemoveAttribute(this XmlNode node, string key)
        {
            return false;
        }
        public static string GetAttributeValue(this XmlNode node, string key)
        {
            return null;
        }

    }
}
