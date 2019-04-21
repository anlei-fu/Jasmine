using Jasmine.Parsers.Html;
using Jasmine.Parsers.Json;
using Jasmine.Spider.Interface;
using Jasmine.Spider.PageExtractor.Models;
using System.Collections.Generic;

namespace Jasmine.Spider.PageExtractor.Model
{
    public class DefaultExtractor : IExtractor
    {
        private ISelector _selector;
        public JsonObJect Extract(Node node, Element element)
        {
            var obj = new JsonObJect();

            switch (node.ChildrenModel)
            {
                case ChildrenModel.Iterate:

                    foreach (var item in _selector.SelectCollection(node.Selector,element))
                        obj.Add(Extract(node.Children[0], item));
                    
                    break;
                case ChildrenModel.Serial:

                    foreach (var item in node.Children)
                    {
                        var result = Extract(item, _selector.SelectOne(item.Selector, element));

                        if (result != null)
                            obj.Add(result);
                    }

                    break;
                case ChildrenModel.Branch:
                    break;
                case ChildrenModel.IterateSerial:
                    break;
                case ChildrenModel.IterateBrach:
                    break;
                default:
                    break;
            }

        }


        

        private IEnumerable<Element> selectCollection()
        {
            return null;
        }
    }
}
