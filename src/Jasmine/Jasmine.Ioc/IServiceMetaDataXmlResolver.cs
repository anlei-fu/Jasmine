namespace Jasmine.Ioc
{
    public  interface IServiceMetaDataXmlResolver
    {
        /// <summary>
        /// load service metaData from a xml configration file
        /// its priority is heigher than <see cref="IServiceMetaDataReflectResolver"/>
        /// </summary>
        /// <param name="path"></param>
        void Resolve(string path);
    }
}
