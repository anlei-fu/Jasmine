using Jasmine.Orm.Model;
using System;

namespace Jasmine.Orm.Exceptions
{
    public  class SqlGrammerNotSurportedException:Exception
    {
        public SqlGrammerNotSurportedException(DataSourceType dataSource,string grammer)
        {
            _dataSource = dataSource;
            _grammer = grammer;
        }

        private DataSourceType _dataSource;
        private string _grammer;

        public override string ToString()
        {
            return $" {_grammer} is not surpported in data server : {_dataSource} !";
        }
    }
}
