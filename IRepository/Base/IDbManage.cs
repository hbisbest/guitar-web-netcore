using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.Base
{
    public interface IDbManage
    {
        #region 数据库同步操作

        int Execute(string sql, object param = null, CommandType commandType = CommandType.Text);

        T ExecuteScalar<T>(string sql, object param = null, CommandType commandType = CommandType.Text);

        IEnumerable<dynamic> Query(string sql, object param = null, CommandType commandType = CommandType.Text);

        IEnumerable<T> Query<T>(string sql, object param = null, CommandType commandType = CommandType.Text);

        dynamic QuerySingle(string sql, object param = null, CommandType commandType = CommandType.Text);

        T QuerySingle<T>(string sql, object param = null, CommandType commandType = CommandType.Text);

        IEnumerable<T> QueryMultiple<T>(string querySql, string countSql, out int count, object param = null);

        IEnumerable<T> GetList<T>(int topCount, string columns, string tableName, string where, string orderBy);

        int GetCount(string tableName, string where = "");

        IEnumerable<T> GetPagerList<T>(string columns, string table, string where, string orderBy, out int rowCount,
            int pageIndex = 1, int pageSize = 10);

        IEnumerable<TReturn> GetPagerList<TFirst, TSecond, TReturn>(string columns, string table, string where,
            string orderBy, Func<TFirst, TSecond, TReturn> map, out int rowCount, int pageIndex = 1, int pageSize = 10);

        IEnumerable<T> GetPagerList<T>(string columns, string table, string where, string orderBy, Type[] types,
            Func<object[], T> map, out int rowCount, int pageIndex = 1, int pageSize = 10);

        #endregion

        #region 数据库异步操作

        Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.Text);

        Task<T> ExecuteScalarAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text);

        Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, CommandType commandType = CommandType.Text);

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text);

        Task<dynamic> QuerySingleAsync(string sql, object param = null, CommandType commandType = CommandType.Text);

        Task<T> QuerySingleAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text);

        Task<IEnumerable<T>> QueryMultipleAsync<T>(string querySql, string countSql, out int count, object param = null);

        Task<IEnumerable<T>> GetListAsync<T>(int topCount, string columns, string tableName, string where, string orderBy);

        Task<int> GetCountAsync(string tableName, string where = "");

        Task<IEnumerable<T>> GetPagerListAsync<T>(string columns, string table, string where, string orderBy, out int rowCount,
            int pageIndex = 1, int pageSize = 10);

        Task<IEnumerable<TReturn>> GetPagerListAsync<TFirst, TSecond, TReturn>(string columns, string table, string where,
            string orderBy, Func<TFirst, TSecond, TReturn> map, out int rowCount, int pageIndex = 1, int pageSize = 10);

        Task<IEnumerable<T>> GetPagerListAsync<T>(string columns, string table, string where, string orderBy, Type[] types,
            Func<object[], T> map, out int rowCount, int pageIndex = 1, int pageSize = 10);

        #endregion
    }
}
