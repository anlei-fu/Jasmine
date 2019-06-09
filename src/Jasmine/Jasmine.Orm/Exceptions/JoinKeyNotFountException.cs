using System;

namespace Jasmine.Orm.Exceptions
{
    public  class JoinKeyNotFountException:Exception
    {
        public JoinKeyNotFountException(Orm.TableMetaData main, Orm.TableMetaData join,string joinKey)
              :base($"join table ({join.RelatedType})-- join-key({joinKey}) can not be found in main table ({main.RelatedType})")
        {

        }
    }
}
