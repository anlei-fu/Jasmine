using Jasmine.Parsers.Html;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Spider.PageExtractor
{
  public  interface ISelector
    {
        IEnumerable<Element> SelectCollection(string seletor,Element element);
        Element SelectOne(string selector,Element element);
    }
}
