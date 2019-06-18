using Jasmine.Common;

namespace Jasmine.Restful
{
    public  class RestfulFilterMetaDataManager:AbstractMetadataManager<FilterMetaData>
    {
        private RestfulFilterMetaDataManager()
        {

        }

        public static readonly RestfulFilterMetaDataManager Instance = new RestfulFilterMetaDataManager();
    }
}
