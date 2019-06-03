using Jasmine.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jasmine.Orm.Interfaces
{
    public interface ISqlExcuter
    {
        IEnumerable<T> Query<T>(string sql);
        IEnumerable<T> Query<T>(string template, object obj);
        IEnumerable<T> Query<T>(Template template, object obj);
        IEnumerable<T> QueryConditional<T>(string condition);

        IEnumerable<T> QueryConditionalWith<T>(string table, string condition);

        IEnumerable<T> QueryOrderByAsc<T>(string orderBy);

        IEnumerable<T> QueryOrderByAscWith<T>(string table, string orderBy);

        IEnumerable<T> QueryOrderByDesc<T>(string orderBy);

        IEnumerable<T> QueryOrderByDescWith<T>(string table, string orderBy);

        IEnumerable<T> QueryPartial<T>(params string[] columns);

        IEnumerable<T> QueryPartialConditional<T>(string condition, params string[] columns);

        IEnumerable<T> QueryPartialConditionalOrderByAsc<T>(string condition, string orderBy, params string[] columns);

        IEnumerable<T> QueryPartialConditionalOrderByAscWith<T>(string table, string condition, string orderBy, params string[] columns);

        IEnumerable<T> QueryPartialConditionalOrderByDesc<T>(string condition, string orderBy, params string[] columns);

        IEnumerable<T> QueryPartialConditionalOrderByDescWith<T>(string table, string condition, string orderBy, params string[] columns);

        IEnumerable<T> QueryPartialConditionalWith<T>(string table, string condition, params string[] columns);

        IEnumerable<T> QueryPartialOrderByAsc<T>(string orderBy, params string[] columns);

        IEnumerable<T> QueryPartialOrderByAscWith<T>(string table, string orderBy, params string[] columns);

        IEnumerable<T> QueryPartialOrderByDesc<T>(string orderBy, params string[] columns);

        IEnumerable<T> QueryPartialOrderByDescWith<T>(string table, string orderBy, params string[] columns);

        IEnumerable<T> QueryPartialWith<T>(string table, params string[] columns);

        IEnumerable<T> QueryTop<T>(int count);

        IEnumerable<T> QueryTopConditional<T>(int count, string condition);

        IEnumerable<T> QueryTopConditionalWith<T>(string table, int count, string condition);

        IEnumerable<T> QueryTopOrderByAsc<T>(int count, string orderBy);

        IEnumerable<T> QueryTopOrderByAscWith<T>(string table, int count, string orderBy);

        IEnumerable<T> QueryTopOrderByDesc<T>(int count, string orderBy);

        IEnumerable<T> QueryTopOrderByDescWith<T>(string table, int count, string orderBy);

        IEnumerable<T> QueryTopPartial<T>(int count, params string[] columns);

        IEnumerable<T> QueryTopPartialConditional<T>(int count, string condition, params string[] columns);

        IEnumerable<T> QueryTopPartialConditionalOrderByAsc<T>(int count, string condition, string orderBy, params string[] columns);

        IEnumerable<T> QueryTopPartialConditionalOrderByAscWith<T>(string table, int count, string condition, string orderBy, params string[] columns);

        IEnumerable<T> QueryTopPartialConditionalOrderByDesc<T>(int count, string condition, string orderBy, params string[] columns);

        IEnumerable<T> QueryTopPartialConditionalOrderByDescWith<T>(string table, int count, string condition, string orderBy, params string[] columns);

        IEnumerable<T> QueryTopPartialConditionalWith<T>(string table, int count, string conditiona, params string[] columns);

        IEnumerable<T> QueryTopPartialOrderByAsc<T>(int count, string orderBy, params string[] columns);

        IEnumerable<T> QueryTopPartialOrderByAscWith<T>(string table, int count, string orderBy, params string[] columns);

        IEnumerable<T> QueryTopPartialOrderByDesc<T>(int count, string orderBy, params string[] columns);

        IEnumerable<T> QueryTopPartialOrderByDescWith<T>(string table, int count, string orderBy, params string[] columns);

        IEnumerable<T> QueryTopPartialWidth<T>(string table, int count, params string[] column);

        IEnumerable<T> QueryTopWith<T>(int count);

        IEnumerable<T> QueryWith<T>(string table);
        Task<IEnumerable<T>> QueryAsync<T>(string sql);

        Task<IEnumerable<T>> QueryAsync<T>(string template, object obj);

        Task<IEnumerable<T>> QueryAsync<T>(Template template, object obj);

        Task<IEnumerable<T>> QueryConditionalAsync<T>(string condition);

        Task<IEnumerable<T>> QueryConditionalWithAsync<T>(string table, string condition);

        Task<IEnumerable<T>> QueryOrderByAscAsync<T>(string orderBy);

        Task<IEnumerable<T>> QueryOrderByAscWithAsync<T>(string table, string orderBy);

        Task<IEnumerable<T>> QueryOrderByDescAsync<T>(string orderBy);

        Task<IEnumerable<T>> QueryOrderByDescWithAsync<T>(string table, string orderBy);

        Task<IEnumerable<T>> QueryPartialAsync<T>(params string[] columns);

        Task<IEnumerable<T>> QueryPartialConditionalAsync<T>(string condition, params string[] columns);

        Task<IEnumerable<T>> QueryPartialConditionalOrderByAscAsync<T>(string condition, string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryPartialConditionalOrderByAscWithAsync<T>(string table, string condition, string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryPartialConditionalOrderByDescAsync<T>(string condition, string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryPartialConditionalOrderByDescWithAsync<T>(string table, string condition, string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryPartialConditionalWithAsync<T>(string table, string condition, params string[] columns);

        Task<IEnumerable<T>> QueryPartialOrderByAscAsync<T>(string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryPartialOrderByAscWithAsync<T>(string table, string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryPartialOrderByDescAsync<T>(string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryPartialOrderByDescWithAsync<T>(string table, string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryPartialWithAsync<T>(string table, params string[] columns);

        Task<IEnumerable<T>> QueryTopAsync<T>(int count);

        Task<IEnumerable<T>> QueryTopConditionalAsync<T>(int count, string condition);

        Task<IEnumerable<T>> QueryTopConditionalWithAsync<T>(string table, int count, string condition);

        Task<IEnumerable<T>> QueryTopOrderByAscAsync<T>(int count, string orderBy);

        Task<IEnumerable<T>> QueryTopOrderByAscWithAsync<T>(string table, int count, string orderBy);

        Task<IEnumerable<T>> QueryTopOrderByDescAsync<T>(int count, string orderBy);

        Task<IEnumerable<T>> QueryTopOrderByDescWithAsync<T>(string table, int count, string orderBy);

        Task<IEnumerable<T>> QueryTopPartialAsync<T>(int count, params string[] columns);

        Task<IEnumerable<T>> QueryTopPartialConditionalAsync<T>(int count, string condition, params string[] columns);

        Task<IEnumerable<T>> QueryTopPartialConditionalOrderByAscAsync<T>(int count, string condition, string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryTopPartialConditionalOrderByAscWithAsync<T>(string table, int count, string condition, string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryTopPartialConditionalOrderByDescAsync<T>(int count, string condition, string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryTopPartialConditionalOrderByDescWithAsync<T>(string table, int count, string condition, string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryTopPartialConditionalWithAsync<T>(string table, int count, string conditiona, params string[] columns);

        Task<IEnumerable<T>> QueryTopPartialOrderByAscAsync<T>(int count, string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryTopPartialOrderByAscWithAsync<T>(string table, int count, string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryTopPartialOrderByDescAsync<T>(int count, string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryTopPartialOrderByDescWithAsync<T>(string table, int count, string orderBy, params string[] columns);

        Task<IEnumerable<T>> QueryTopPartialWidthAsync<T>(string table, int count, params string[] column);

        Task<IEnumerable<T>> QueryTopWithAsync<T>(int count);

        Task<IEnumerable<T>> QueryWithAsync<T>(string table);
        int BatchInsert<T>(IEnumerable<T> data,bool transanction=false);
        int BatchInsert<T>(string template, IEnumerable<T> data,bool transanction= false);

        int BatchInsertWith<T>(string table, IEnumerable<T> datas, bool transanction = false);

        int BatchInsertWith<T>(string table, string template, IEnumerable<T> datas, bool transanction = false);

        int Create<T>();

        int CreateWith<T>(string name);

        int Delete(string table, string condition, bool transanction = false);

        int Delete<T>(string condition, bool transanction = false);

        int Drop(string name);

        int Drop<T>();

        int Excute(string sql, bool transanction = false);

        int Excute(string template, object obj, bool transanction = false);

        int Excute(Template template, object obj, bool transanction = false);

        int Insert<T>(string template, T data);

        int Insert<T>(T data);

        int InsertWith<T>(string table, string template, T data);

        int InsertWith<T>(string table, T data);

        int Update<T>(object parameter);

        int UpdateConditional<T>(string condition, object parameter);

        int UpdateConditionalWidth<T>(string table, string condition, object parameter);

        int UpdateWith<T>(string table, object parameter);
        ICursor QueryCursor<T>();
        ICursor QueryCursor<T>(string sql);
        ICursor QueryCursor<T>(string template, object obj);
        ICursor QueryCursor<T>(Template template, object obj);
        ICursor QueryConditionalCursor<T>(string condition);
        ICursor QueryConditionalWithCursor<T>(string table, string condition);
        ICursor QueryOrderByAscCursor<T>(string orderBy);
        ICursor QueryOrderByAscWithCursor<T>(string table, string orderBy);
        ICursor QueryOrderByDescCursor<T>(string orderBy);
        ICursor QueryOrderByDescWithCursor<T>(string table, string orderBy);
        ICursor QueryPartialCursor<T>(params string[] columns);
        ICursor QueryPartialConditionalCursor<T>(string condition, params string[] columns);
        ICursor QueryPartialConditionalOrderByAscCursor<T>(string condition, string orderBy, params string[] columns);
        ICursor QueryPartialConditionalOrderByAscWithCursor<T>(string table, string condition, string orderBy, params string[] columns);
        ICursor QueryPartialConditionalOrderByDescCursor<T>(string condition, string orderBy, params string[] columns);
        ICursor QueryPartialConditionalOrderByDescWithCursor<T>(string table, string condition, string orderBy, params string[] columns);
        ICursor QueryPartialConditionalWithCursor<T>(string table, string condition, params string[] columns);
        ICursor QueryPartialOrderByAscCursor<T>(string orderBy, params string[] columns);
        ICursor QueryPartialOrderByAscWithCursor<T>(string table, string orderBy, params string[] columns);
        ICursor QueryPartialOrderByDescCursor<T>(string orderBy, params string[] columns);
        ICursor QueryPartialOrderByDescWithCursor<T>(string table, string orderBy, params string[] columns);
        ICursor QueryPartialWithCursor<T>(string table, params string[] columns);
        ICursor QueryTopCursor<T>(int count);
        ICursor QueryTopConditionalCursor<T>(int count, string condition);
        ICursor QueryTopConditionalWithCursor<T>(string table, int count, string condition);
        ICursor QueryTopOrderByAscCursor<T>(int count, string orderBy);
        ICursor QueryTopOrderByAscWithCursor<T>(string table, int count, string orderBy);
        ICursor QueryTopOrderByDescCursor<T>(int count, string orderBy);
        ICursor QueryTopOrderByDescWithCursor<T>(string table, int count, string orderBy);
        ICursor QueryTopPartialCursor<T>(int count, params string[] columns);
        ICursor QueryTopPartialConditionalCursor<T>(int count, string condition, params string[] columns);
        ICursor QueryTopPartialConditionalOrderByAscCursor<T>(int count, string condition, string orderBy, params string[] columns);
        ICursor QueryTopPartialConditionalOrderByAscWithCursor<T>(string table, int count, string condition, string orderBy, params string[] columns);
        ICursor QueryTopPartialConditionalOrderByDescCursor<T>(int count, string condition, string orderBy, params string[] columns);
        ICursor QueryTopPartialConditionalOrderByDescWithCursor<T>(string table, int count, string condition, string orderBy, params string[] columns);
        ICursor QueryTopPartialConditionalWithCursor<T>(string table, int count, string conditiona, params string[] columns);
        ICursor QueryTopPartialOrderByAscCursor<T>(int count, string orderBy, params string[] columns);
        ICursor QueryTopPartialOrderByAscWithCursor<T>(string table, int count, string orderBy, params string[] columns);
        ICursor QueryTopPartialOrderByDescCursor<T>(int count, string orderBy, params string[] columns);
        ICursor QueryTopPartialOrderByDescWithCursor<T>(string table, int count, string orderBy, params string[] columns);
        ICursor QueryTopPartialWidthCursor<T>(string table, int count, params string[] column);
        ICursor QueryTopWithCursor<T>(int count);
        ICursor QueryWithCursor<T>(string table);
        IEnumerable<T> Query<T>();
        Task<ICursor> QueryAsync<T>();
        Task<ICursor> QueryCursorAsync<T>(string sql);
        Task<ICursor> QueryCursorAsync<T>(string template, object obj);
        Task<ICursor> QueryCursorAsync<T>(Template template, object obj);
        Task<ICursor> QueryConditionalCursorAsync<T>(string condition);
        Task<ICursor> QueryConditionalWithCursorAsync<T>(string table, string condition);
        Task<ICursor> QueryOrderByAscCursorAsync<T>(string orderBy);
        Task<ICursor> QueryOrderByAscWithCursorAsync<T>(string table, string orderBy);
        Task<ICursor> QueryOrderByDescCursorAsync<T>(string orderBy);
        Task<ICursor> QueryOrderByDescWithCursorAsync<T>(string table, string orderBy);
        Task<ICursor> QueryPartialvAsync<T>(params string[] columns);
        Task<ICursor> QueryPartialConditionalCursorAsync<T>(string condition, params string[] columns);
        Task<ICursor> QueryPartialConditionalOrderByAscCursorAsync<T>(string condition, string orderBy, params string[] columns);
        Task<ICursor> QueryPartialConditionalOrderByAscWithCursorAsync<T>(string table, string condition, string orderBy, params string[] columns);
        Task<ICursor> QueryPartialConditionalOrderByDescCursorAsync<T>(string condition, string orderBy, params string[] columns);
        Task<ICursor> QueryPartialConditionalOrderByDescWithCursorAsync<T>(string table, string condition, string orderBy, params string[] columns);
        Task<ICursor> QueryPartialConditionalWithCursorAsync<T>(string table, string condition, params string[] columns);
        Task<ICursor> QueryPartialOrderByAscCursorAsync<T>(string orderBy, params string[] columns);
        Task<ICursor> QueryPartialOrderByAscWithCursorAsync<T>(string table, string orderBy, params string[] columns);
        Task<ICursor> QueryPartialOrderByDescCursorAsync<T>(string orderBy, params string[] columns);
        Task<ICursor> QueryPartialOrderByDescWithCursorAsync<T>(string table, string orderBy, params string[] columns);
        Task<ICursor> QueryPartialWithCursorAsync<T>(string table, params string[] columns);
        Task<ICursor> QueryTopCursorAsync<T>(int count);
        Task<ICursor> QueryTopConditionalCursorAsync<T>(int count, string condition);
        Task<ICursor> QueryTopConditionalWithCursorAsync<T>(string table, int count, string condition);
        Task<ICursor> QueryTopOrderByAscCursorAsync<T>(int count, string orderBy);
        Task<ICursor> QueryTopOrderByAscWithCursorAsync<T>(string table, int count, string orderBy);
        Task<ICursor> QueryTopOrderByDescCursorAsync<T>(int count, string orderBy);
        Task<ICursor> QueryTopOrderByDescWithCursorAsync<T>(string table, int count, string orderBy);
        Task<ICursor> QueryTopPartialCursorAsync<T>(int count, params string[] columns);
        Task<ICursor> QueryTopPartialConditionalCursorAsync<T>(int count, string condition, params string[] columns);
        Task<ICursor> QueryTopPartialConditionalOrderByAscCursorAsync<T>(int count, string condition, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialConditionalOrderByAscWithCursorAsync<T>(string table, int count, string condition, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialConditionalOrderByDescvAsync<T>(int count, string condition, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialConditionalOrderByDescWithCursorAsync<T>(string table, int count, string condition, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialConditionalWithCursorAsync<T>(string table, int count, string conditiona, params string[] columns);
        Task<ICursor> QueryTopPartialOrderByAscCursorAsync<T>(int count, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialOrderByAscWithCursorAsync<T>(string table, int count, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialOrderByDescCursorAsync<T>(int count, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialOrderByDescWithvAsync<T>(string table, int count, string orderBy, params string[] columns);
        Task<ICursor> QueryTopPartialWidthCursorAsync<T>(string table, int count, params string[] column);
        Task<ICursor> QueryTopWithCursorAsync<T>(int count);
        Task<ICursor> QueryWithCursorAsync<T>(string table);
        Task<int> BatchInsertAsync<T>(IEnumerable<T> data, bool transanction = false);
        Task<int> BatchInsertAsync<T>(string template, IEnumerable<T> data, bool transanction = false);
        Task<int> BatchInsertWithAsync<T>(string table, IEnumerable<T> datas, bool transanction = false);
        Task<int> BatchInsertWithAsync<T>(string table, string template, IEnumerable<T> datas, bool transanction = false);
        Task<int> CreateAsync<T>();
        Task<int> CreateWithAsync<T>(string name);
        Task<int> DeleteAync(string table, string condition);
        Task<int> DeleteAsync<T>(string condition);
        Task<int> DropAsync(string name);
        Task<int> DropAsync<T>();
        Task<int> ExcuteAsync(string sql, bool transanction = false);
        Task<int> ExcuteAsync(string template, object obj, bool transanction = false);
        Task<int> ExcuteAsyn(Template template, object obj, bool transanction = false);
        Task<int> InsertAsync<T>(string template, T data);
        Task<int> InsertAsync<T>(T data);
        Task<int> InsertWithAsync<T>(string table, string template, T data);
        Task<int> InsertWithAsync<T>(string table, T data);
        Task<int> UpdateAsync<T>(object parameter);
        Task<int> UpdateConditionalAsync<T>(string condition, object parameter);
        Task<int> UpdateConditionalWidthAsync<T>(string table, string condition, object parameter);
        Task<int> UpdateWithAsync<T>(string table, object parameter);

    }
}
