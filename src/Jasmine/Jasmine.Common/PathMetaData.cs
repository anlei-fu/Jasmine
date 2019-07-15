using Jasmine.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Common
{

    public class PathMetaData
    {
        private PathMetaData()
        {

        }

        public static PathMetaData Parse(string raw)
        {
            var path = new PathMetaData();

        }

        public bool MatchAndExtract(string raw)
        {
            var segs = raw.Splite1WithCount("/");

            return true;
        }

        public IDictionary<string,string>Extract()
        {
            return null;
        }

        public string RawString { get; set; }
        public bool HasVarible { get; set; }

        public PathSegment[] Segments { get; set; }
        public class PathSegment
        {
            public string Value { get; set; }
            public bool IsVarible { get; set; }
        }

    }
}
