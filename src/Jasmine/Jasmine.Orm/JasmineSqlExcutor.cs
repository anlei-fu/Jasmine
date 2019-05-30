using Jasmine.Configuration;
using Jasmine.Orm.Interfaces;
using System;
using System.Collections.Generic;

namespace Jasmine.Orm.Implements
{
    public class JasmineSqlExcutor : ISqlExcuter
    {
        public int BatchInsert<T>(IEnumerable<T> data, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public int BatchInsert<T>(IEnumerable<T> datas, string table, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public int Create(string name, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public int Create<T>(string name, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public int Drop<T>(IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public int Drop<T>(string name, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public int Excute(string sql, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public int Excute(string template, object obj, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public int Excute(Template template, object obj, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public int Insert<T>(T data, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public int Insert<T>(T data, string table, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query<T>(string sql, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEnumerable<object>> Query(string sql, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query<T>(string template, object obj, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEnumerable<object>> Query(string template, object obj, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query<T>(Template template, object obj, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEnumerable<object>> Query(Template template, object obj, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query<T>(IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryCursor(string sql, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryCursor(string template, object obj, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryCursor(Template template, object obj, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryCursor<T>(IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public ICursor QueryCursor<T>(string table, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryWith<T>(string table, IDbConnectionProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}
