using System;
using System.Collections.Generic;
using System.Text;

namespace IRepository.Base
{
    public interface IRepository<TEntity> : IBaseRepository<TEntity>, IDbManage where TEntity : class, new()
    {
    }
}
