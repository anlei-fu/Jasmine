namespace Jasmine.Orm
{
    public interface ITableTemplateCache
    {
        string GetCreate();
        string GetCreateWith(string table);
        /*
         *  full
         */
        string GetQuery();
        string GetQueryWith(string table);
        string GetQueryOrderByAsc(string orderBy);
        string GetQueryOrderByAscWith(string table,string orderBy);
        string GetQueryOrderByDesc(string orderBy);
        string GetQueryOrderByDescWith(string table,string orderBy);

        /*
         * full conditional
         */
        string GetQueryConditional(string condition);
        string GetQueryConditionalWith(string table,string condition);
        string GetQueryConditionalOrderByAsc(string condition, string orderBy);
        string GetQueryConditionalOrderByAscWith(string table,string condition, string orderBy);
        string GetQueryConditionalOrderByDesc(string condition, string orderBy);
        string GetQueryConditionalOrderByDescWith(string table,string condition, string orderBy);

        /*
         * partial
         */
        //full
        string GetQueryPartial(params string[] column);
        string GetQueryPartialWith(string table,params string[] column);
        string GetQueryPartialOrderByAsc(string orderBy,params string[] column);
        string GetQueryPartialOrderByAscWith(string table,string orderBy, params string[] column);
        string GetQueryPartialOrderByDesc(string orderBy, params string[] column);
        string GetQueryPartialOrderByDescWith(string table,string orderBy, params string[] column);

        //partial conditional
        string GetQueryPartialConditional(string condition, params string[] columns);
        string GetQueryPartialConditionalWith(string table,string condition, params string[] columns);
        string GetQueryPartialConditionalOrderByAsc(string condition, string orderby,params string[] columns);
        string GetQueryPartialConditionalOrderByAscWith(string table,string condition, string orderby, params string[] columns);
        string GetQueryPartialConditionalOrderByDesc(string condition, string orderby,params string[] columns);
        string GetQueryPartialConditionalOrderByDescWith(string table,string condition, string orderby, params string[] columns);



        /*
         * top
         */
        //top full
        string GetQueryTop(int count);
        string GetQueryTopWith(string table,int count);
        string GetQueryTopOrderByAsc(int count, string orderBy);
        string GetQueryTopOrderByAscWith(string table,int count, string orderBy);
        string GetQueryTopOrderByDesc(int count, string orderBy);
        string GetQueryTopOrderByDescWith(string table,int count, string orderBy);
        //top conditional
        string GetQueryTopConditional(int count, string condition);
        string GetQueryTopConditionalWith(string table,int count, string condition);
        string GetQueryTopConditionalOrderByAsc(int count, string orderBy, string condition);
        string GetQueryTopConditionalOrderByAscWith(string table,int count, string orderBy, string condition);
        string GetQueryTopConditionalOrderByDesc(int count, string orderBy, string condition);
        string GetQueryTopConditionalOrderByDescWith(string table,int count, string orderBy, string condition);
        //top partial
        string GetQueryTopPartial(int count, params string[] condition);
        string GetQueryTopPartialWith(string table,int count, params string[] condition);
        string GetQueryTopParitialOrderByAsc(int count, string orderBy, params string[] columns);
        string GetQueryTopParitialOrderByAscWith(string table,int count, string orderBy, params string[] columns);
        string GetQueryTopPartialOrderByDesc(int count, string orderBy, params string[] columns);
        string GetQueryTopPartialOrderByDescWith(string table,int count, string orderBy, params string[] columns);
        // top partil conditional
        string GetQueryTopPartialConditional(int count, string condition, params string[] columns);
        string GetQueryTopPartialConditionalWith(string table,int count, string condition, params string[] columns);
        string GetQueryTopPartialConditionalOrderByAsc(int count, string condition,string orderBy, params string[] columns);
        string GetQueryTopPartialConditionalOrderByAscWith(string table,int count, string condition, string orderBy, params string[] columns);
        string GetQueryTopPartialConditionalOrderByDesc(int count, string condition, string orderBy, params string[] columns);
        string GetQueryTopPartialConditionalOrderByDescWith(string table,int count, string condition, string orderBy, params string[] columns);



        SqlTemplate GetInsert();
        SqlTemplate GetInsert(string prefix);
        SqlTemplate GetInsertWith(string table);
        SqlTemplate GetInsertPartial(params string[] columns);
        SqlTemplate GetInsertPartialWith(string table, params string[] columns);






        string GetDelete(string condition);
        string GetDeleteWith(string table,string condition);

        string GetDrop();
        string GetDropWith(string table);

     
        
    }
}
