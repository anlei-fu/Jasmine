namespace Jasmine.Common
{
    public   interface IParamteterResolver<T>
    {
        /// <summary>
        ///  resolve parameters from context
        /// </summary>
        /// <param name="context"> context</param>
        /// <returns> resolved parameters</returns>
        object[] Resolve(T context);
    }
}
