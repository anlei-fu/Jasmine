namespace Jasmine.Orm
{
    public interface ITableTemplateCache
    {
        SqlTemplate GetCreate();
        SqlTemplate GetCreateWith(string table);
        /*
         *  full
         */
        SqlTemplate GetQuery();
        SqlTemplate GetQueryWith(string table);
        SqlTemplate GetQueryOrderByAsc(string table,string orderBy);
        SqlTemplate GetQueryOrderByAscWith(string orderBy);
        SqlTemplate GetQueryOrderByDesc(string orderBy);
        SqlTemplate GetQueryOrderByDescWith(string table,string orderBy);

        /*
         * full conditional
         */
        SqlTemplate GetQueryConditional(string condition);
        SqlTemplate GetQueryConditionalWith(string table,string condition);
        SqlTemplate GetQueryConditionalOrderByAsc(string condition, string orderBy);
        SqlTemplate GetQueryConditionalOrderByAscWith(string table,string condition, string orderBy);
        SqlTemplate GetQueryConditionalOrderByDesc(string condition, string orderBy);
        SqlTemplate GetQueryConditionalOrderByDescWith(string table,string condition, string orderBy);

        /*
         * partial
         */
        //full
        SqlTemplate GetQueryPartial(params string[] column);
        SqlTemplate GetQueryPartialWith(string table,params string[] column);
        SqlTemplate GetQueryPartialOrderByAsc(string orderBy,params string[] column);
        SqlTemplate GetQueryPartialOrderByAscWith(string table,string orderBy, params string[] column);
        SqlTemplate GetQueryPartialOrderByDesc(string orderBy, params string[] column);
        SqlTemplate GetQueryPartialOrderByDescWith(string table,string orderBy, params string[] column);

        //partial conditional
        SqlTemplate GetQueryPartialConditional(string condition, params string[] columns);
        SqlTemplate GetQueryPartialConditionalWith(string table,string condition, params string[] columns);
        SqlTemplate GetQueryPartialConditionalOrderByAsc(string condition, string orderby,params string[] columns);
        SqlTemplate GetQueryPartialConditionalOrderByAscWith(string table,string condition, string orderby, params string[] columns);
        SqlTemplate GetQueryPartialConditionalOrderByDesc(string condition, string orderby,params string[] columns);
        SqlTemplate GetQueryPartialConditionalOrderByDescWith(string table,string condition, string orderby, params string[] columns);



        /*
         * top
         */
        //top full
        SqlTemplate GetQueryTop(int count);
        SqlTemplate GetQueryTopWith(string table,int count);
        SqlTemplate GetQueryTopOrderByAsc(int count, string orderBy);
        SqlTemplate GetQueryTopOrderByAscWith(string table,int count, string orderBy);
        SqlTemplate GetQueryTopOrderByDesc(int count, string orderBy);
        SqlTemplate GetQueryTopOrderByDescWith(string table,int count, string orderBy);
        //top conditional
        SqlTemplate GetQueryTopConditional(int count, string condition);
        SqlTemplate GetQueryTopConditionalWith(string table,int count, string condition);
        SqlTemplate GetQueryTopConditionalOrderByAsc(int count, string orderBy, string condition);
        SqlTemplate GetQueryTopConditionalOrderByAscWith(string table,int count, string orderBy, string condition);
        SqlTemplate GetQueryTopConditionalOrderByDesc(int count, string orderBy, string condition);
        SqlTemplate GetQueryTopConditionalOrderByDescWith(string table,int count, string orderBy, string condition);
        //top partial
        SqlTemplate GetQueryTopPartial(int count, params string[] condition);
        SqlTemplate GetQueryTopPartialWith(string table,int count, params string[] condition);
        SqlTemplate GetQueryTopPaitialOrderByAsc(int count, string orderBy, params string[] columns);
        SqlTemplate GetQueryTopPaitialOrderByAscWith(string table,int count, string orderBy, params string[] columns);
        SqlTemplate GetQueryTopPartialOrderByDesc(int count, string orderBy, params string[] columns);
        SqlTemplate GetQueryTopPartialOrderByDescWith(string table,int count, string orderBy, params string[] columns);
        // top partil conditional
        SqlTemplate GetQueryTopPartialConditional(int count, string condition, params string[] columns);
        SqlTemplate GetQueryTopPartialConditional(string table,int count, string condition, params string[] columns);
        SqlTemplate GetQueryTopPartialConditionalOrderByAsc(int count, string condition,string orderBy, params string[] columns);
        SqlTemplate GetQueryTopPartialConditionalOrderByAscWith(string table,int count, string condition, string orderBy, params string[] columns);
        SqlTemplate GetQueryTopPartialConditionalOrderByDesc(int count, string condition, string orderBy, params string[] columns);
        SqlTemplate GetQueryTopPartialConditionalOrderByDescWith(string table,int count, string condition, string orderBy, params string[] columns);



        SqlTemplate GetInsert();
        SqlTemplate GetInsertWith(string table);
        SqlTemplate GetInsertPartial(params string[] columns);
        SqlTemplate GetInsertPartialWith(string table, params string[] columns);






        SqlTemplate GetDelete(string condition);
        SqlTemplate GetDeleteWith(string table,string condition);

        SqlTemplate GetDrop();
        SqlTemplate GetDropWith(string table);

        SqlTemplate GetUpdate();

        SqlTemplate GetUpdateWith();
        SqlTemplate GetQueryOrderByAsc(string orderBy);
    }
}
