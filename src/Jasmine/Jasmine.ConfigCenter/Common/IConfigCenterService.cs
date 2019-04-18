namespace Jasmine.ConfigCenter.Common
{
    public  interface IConfigCenterService
    {
        ConfigCenterServiceResponse Call(ConfigCenterServiceRequest request);
    }
}
