namespace Jasmine.Orm
{
    public interface ITableOrdinaryOperateTemplateGenarator
    {
        SqlTemplate GenerateCreateTable(TableMetaData metadata);
        SqlTemplate GenerateInsert(TableMetaData metadata);
        SqlTemplate GenerateSelectAll(TableMetaData metaData);
        SqlTemplate GenerateSelectTopAll(TableMetaData metaData);
        SqlTemplate GenerateSelectDistinctAll(TableMetaData metaData,int count);
        SqlTemplate GenerateSelectTopDistinct(TableMetaData metaData,int count);
        SqlTemplate GenerateDropTable(TableMetaData metaData);
    }
}
