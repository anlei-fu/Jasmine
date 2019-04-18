namespace Jasmine.Restful
{
    public   interface  IServiceMetric
    {
       int MaxConCurrency { get; set; }
       int CurrentConcurrency { get; set; }
       
    }
}
