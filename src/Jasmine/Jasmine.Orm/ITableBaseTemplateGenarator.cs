using Jasmine.Configuration;

namespace Jasmine.Orm
{
    public interface ITableBaseTemplateGenarator
    {
        Template GenerateCreateTable(TableMetaData metadata);
        Template GenerateInsert(TableMetaData metadata);
        Template GenerateSelectAll(TableMetaData metaData);
        Template GenerateSelectTopAll(TableMetaData metaData);
        Template GenerateSelectDistinctAll(TableMetaData metaData,int count);
        Template GenerateSelectTopDistinct(TableMetaData metaData,int count);
        Template GenerateDropTable(TableMetaData metaData);
    }
}
