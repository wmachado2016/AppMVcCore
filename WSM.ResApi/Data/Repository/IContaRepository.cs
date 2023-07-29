using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WSM.ResApi.Models;

namespace WSM.ResApi.Data.Repository
{
    public interface IContaRepository : IRepository<Conta>
    {
        Task<IEnumerable<Conta>> ObterTodos();
        Task<Conta> ObterPorId(Guid id);
        void Adicionar(Conta conta);
        void Atualizar(Conta conta);
        void Remover(Conta conta);
        int SaveChanges();
    }
}
