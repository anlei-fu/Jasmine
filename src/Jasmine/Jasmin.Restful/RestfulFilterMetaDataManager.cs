using Jasmine.Common;
using System;

namespace Jasmine.Restful
{
    public  class RestfulFilterMetaDataManager:AbstractMetadataManager<FilterMetaData>
    {
        private RestfulFilterMetaDataManager()
        {

        }

        public static readonly RestfulFilterMetaDataManager Instance = new RestfulFilterMetaDataManager();

        public void Add(Type type)
        {

        }
    }
}
