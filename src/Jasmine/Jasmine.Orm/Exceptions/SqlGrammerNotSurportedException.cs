using System;

namespace Jasmine.Orm.Exceptions
{
    public  class SqlGrammerNotSurportedException:Exception
    {
        public SqlGrammerNotSurportedException(DataSource dataSource,string grammer)
        {
            _dataSource = dataSource;
            _grammer = grammer;
        }

        private DataSource _dataSource;
        private string _grammer;

        public override string ToString()
        {
            return $" {_grammer} is not surpported in data server : {_dataSource} !";
        }
    }
}
