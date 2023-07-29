using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WSM.ResApi.Models;

namespace WSM.ResApi.Data.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        
    }
}