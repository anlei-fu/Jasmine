using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Restful
{
  public  class RestfulApplicationConfig
    {
        public ListenOption ListenOption { get; set; }
        public SslOption SslOption { get; set; }
        public StaticFileOption StaticFileOption { get; set; }
    }
}
