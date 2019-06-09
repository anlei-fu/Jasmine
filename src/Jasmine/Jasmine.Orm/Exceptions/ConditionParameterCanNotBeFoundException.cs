namespace Jasmine.Orm.Exceptions
{
    public class ConditionParameterCanNotBeFoundException: System.Exception
    {
        public ConditionParameterCanNotBeFoundException(string template,string varible,Orm.TableMetaData main)
              :base($" condition({template}) varible({varible}) can not be found in table({main.RelatedType})")
        {

        }
    }
}
