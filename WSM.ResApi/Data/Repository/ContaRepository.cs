using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WSM.ResApi.Models;

namespace WSM.ResApi.Data.Repository
{
    public class ContaRepository : IContaRepository
    {

        private readonly ApplicationDbContext _context;
        public ContaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Conta> ObterPorId(Guid id)
        {
            return await _context.Contas.FindAsync(id);
        }

        public async Task<IEnumerable<Conta>> ObterTodos()
        {
            return await _context.Contas.AsNoTracking().ToListAsync();
        }
        public void Adicionar(Conta conta)
        {
            _context.Contas.Add(conta);
            this.SaveChanges();
        }

        public void Atualizar(Conta conta)
        {
            _context.Contas.Update(conta);
            this.SaveChanges();
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Remover(Conta conta)
        {
            _context.Contas.Remove(conta);
            this.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
