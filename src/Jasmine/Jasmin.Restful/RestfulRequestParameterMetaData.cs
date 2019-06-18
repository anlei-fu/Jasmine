using Jasmine.Common;

namespace Jasmine.Restful
{
    public   class RestfulRequestParameterMetaData:ParameterMetaDataBase
    {
        /// <summary>
        /// resolve parameter from cookie
        /// </summary>
        public string CookieKey { get; set; }
        public bool FromCookie => CookieKey != null;
        /// <summary>
        /// resolve parameter from header
        /// </summary>
        public string HearderKey { get; set; }
        public bool FromHeader => HearderKey != null;
        /// <summary>
        /// resolve parameter from data <see cref="HttpFilterContext.PipelineDatas"/>
        /// </summary>
        public string DataKey { get; set; }
        public bool FromData => DataKey != null;
        /// <summary>
        /// resolve parameter from body 
        /// </summary>
        public bool FromBody { get; set; }
        /// <summary>
        /// resolve parameter from query string
        /// </summary>
        public string QueryStringKey { get; set; }
        public bool FromQueryString => QueryStringKey != null;
        /// <summary>
        /// resolve parameter from path varible
        /// </summary>
        public string PathVariableKey { get; set; }
        public bool FromPathVariable { get; set; }
        /// <summary>
        /// resolve parameter from form
        /// </summary>
        public string FormKey { get; set; }
        public bool FromForm => FormKey != null;
        /// <summary>
        /// resolve parameter from return
        /// </summary>
        public bool FromReturn { get; set; }
      
        
    }
}
