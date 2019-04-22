using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Jasmine.Orm.Implements
{
    public class JasmineSqlExcutor : ISqlExcuter
    {
        public JasmineSqlExcutor(ILog logger )
        {
            _logger = logger;
        }
        private ILog _logger;
        private ISqlConvertorProvider _convertorProvider=> DefaultSqlConvertorProvider.Instance;
        private ITemplateConvertor _templateBuilder=>DefaultTemplateConvertor.Instance;
        private IUnknowTypeConvertor _unknowTypeConvertor=>DefaultUnkowTypeConvertor.Instance;

        public int Excute(string template, object parameter, IDbConnectionProvider provider)
        {
            return Excute(_templateBuilder.Convert(template, parameter), provider);
        }

        public int Excute(IEnumerable<SqlTemplateSegment> segments, IDictionary<string, object> parameter, IDbConnectionProvider provider)
        {
            return Excute(_templateBuilder.Convert(segments, parameter), provider);
        }

        public int Excute(IEnumerable<SqlTemplateSegment> segments, IEnumerable<IDictionary<string, object>> parameters, IDbConnectionProvider provider)
        {
            return Excute(_templateBuilder.Convert(segments, parameters), provider);
        }
        public int Excute(string sql, IDbConnectionProvider provider)
        {
            var connection = provider.Rent();
            try
            {
                connection.Open();
                var result= new SqlCommand(sql, connection).ExecuteNonQuery();
                connection.Close();

                return result;

            }
            catch (Exception ex)
            {
                _logger?.Error(ex);
                throw ex;
            }
            finally
            {
                provider.Recycle(connection);
            }
        }

        public IEnumerable<T> Query<T>(string sql, IDbConnectionProvider provider)
        {
            var connection = provider.Rent();

            try
            {
                connection.Open();
                var reader = new SqlCommand(sql, connection).ExecuteReader();
                var convertor = _convertorProvider.GetConvertor<T>();
                var context = new SqlResultContext(reader);
                var type = typeof(T);
                var result = new List<T>();

                while (reader.HasRows)
                {
                    if(reader.Read())
                    {
                        result.Add((T)convertor.FromResult(context, type));
                    }
                    else
                    {
                        break;
                    }
                }
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                _logger?.Error(ex);

                throw ex;
            }
            finally
            {
                provider.Recycle(connection);
            }
        }

        public IEnumerable<IEnumerable<object>> Query(string sql, IDbConnectionProvider provider)
        {
            var connection = provider.Rent();

            try
            {
                connection.Open();
                var reader = new SqlCommand(sql, connection).ExecuteReader();
            
                var context = new SqlResultContext(reader);
                var result = new List<IEnumerable<object>>();

                while (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        result.Add(_unknowTypeConvertor.Convert(context));
                    }
                    else
                    {
                        break;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger?.Error(ex);

                throw ex;
            }
            finally
            {
                provider.Recycle(connection);
            }
        }

        public IEnumerable<T> Query<T>(string template, IDictionary<string, object> paramter, IDbConnectionProvider provider)
        {
            return Query<T>(_templateBuilder.Convert(template, paramter), provider);
        }

        public IEnumerable<IEnumerable<object>> Query(string template, IDictionary<string, object> paramter, IDbConnectionProvider provider)
        {
            return Query(_templateBuilder.Convert(template, paramter), provider);
        }

        public IEnumerable<T> Query<T>(string template, IEnumerable<IDictionary<string, object>> parameters, IDbConnectionProvider provider)
        {
            return Query<T>(_templateBuilder.Convert(template, parameters), provider);
        }

        public IEnumerable<IEnumerable<object>> Query(string template, IEnumerable<IDictionary<string, object>> parameters, IDbConnectionProvider provider)
        {
            return Query(_templateBuilder.Convert(template, parameters), provider);
        }

        public IEnumerable<T> Query<T>(IEnumerable<SqlTemplateSegment> segments, IDictionary<string, object> parameter, IDbConnectionProvider provider)
        {
            return Query<T>(_templateBuilder.Convert(segments, parameter), provider);
        }

        public IEnumerable<IEnumerable<object>> Query(IEnumerable<SqlTemplateSegment> segments, IDictionary<string, object> parameters, IDbConnectionProvider provider)
        {
            return Query(_templateBuilder.Convert(segments, parameters), provider);
        }

        public IEnumerable<T> Query<T>(IEnumerable<SqlTemplateSegment> segments, IEnumerable<IDictionary<string, object>> parameters, IDbConnectionProvider provider)
        {
            return Query<T>(_templateBuilder.Convert(segments, parameters), provider);
        }

        public IEnumerable<IEnumerable<object>> Query(IEnumerable<SqlTemplateSegment> segments, IEnumerable<IDictionary<string, object>> parameters, IDbConnectionProvider provider)
        {
            return Query(_templateBuilder.Convert(segments, parameters), provider);
        }

      
        public ICursor QueryCursor(string template, IDictionary<string, object> paramter, IDbConnectionProvider provider)
        {
            return QueryCursor(_templateBuilder.Convert(template, paramter), provider);
        }

        public ICursor QueryCursor(IEnumerable<SqlTemplateSegment> segments, IEnumerable<IDictionary<string, object>> parameters, IDbConnectionProvider provider)
        {
            return QueryCursor(_templateBuilder.Convert(segments, parameters), provider);
        }

        public ICursor QueryCursor(IEnumerable<SqlTemplateSegment> segments, IDictionary<string, object> parameter, IDbConnectionProvider provider)
        {
            return QueryCursor(_templateBuilder.Convert(segments, parameter), provider);
        }

        public ICursor QueryCusor(string template, IEnumerable<IDictionary<string, object>> parameters, IDbConnectionProvider provider)
        {
            return QueryCursor(_templateBuilder.Convert(template, parameters), provider);
        }
        public ICursor QueryCursor(string sql, IDbConnectionProvider provider)
        {
            var connetion = provider.Rent();

            try
            {
                connetion.Open();
                var reader = new SqlCommand(sql, connetion).ExecuteReader();

                return new DefaultCursor(new SqlResultContext(reader), _convertorProvider, connetion, provider);

            }
            catch (Exception ex)
            {
                _logger?.Error(ex);

                throw ex;
            }
        }

    }
}
