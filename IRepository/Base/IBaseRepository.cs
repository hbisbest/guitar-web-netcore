using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        #region 仓储同步方法

        long Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        bool Delete(TEntity entity);

        bool Delete(IEnumerable<TEntity> entities);

        bool Update(TEntity entity);

        bool Update(IEnumerable<TEntity> entities);

        //bool Update(TEntity entity, Func<PropertyInfo, bool> propertyFilter);

        //bool Update(IEnumerable<TEntity> entities, Func<PropertyInfo, bool> propertyFilter);

        //bool Update(TEntity entity, string[] properties, bool isWrite = true);

        //bool Update(IEnumerable<TEntity> entities, string[] properties, bool isWrite = true);

        TEntity Get(object id);

        IEnumerable<TEntity> GetAll();

        #endregion

        #region 仓储异步方法

        Task<long> InsertAsync(TEntity entity);

        Task InsertAsync(IEnumerable<TEntity> entities);

        Task<bool> DeleteAsync(TEntity entity);

        Task<bool> DeleteAsync(IEnumerable<TEntity> entities);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> UpdateAsync(IEnumerable<TEntity> entities);

        //Task<bool> UpdateAsync(TEntity entity, Func<PropertyInfo, bool> propertyFilter);

        //Task<bool> UpdateAsync(IEnumerable<TEntity> entities, Func<PropertyInfo, bool> propertyFilter);

        //Task<bool> UpdateAsync(TEntity entity, string[] properties, bool isWrite = true);

        //Task<bool> UpdateAsync(IEnumerable<TEntity> entities, string[] properties, bool isWrite = true);

        Task<TEntity> GetAsync(object id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        #endregion
    }
}
