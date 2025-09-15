using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Repositorys
{
    public interface IProdutoRepository
    {
        public Task<Produto> AdicionarAsync(Produto produto);
        public Task<List<Produto>> ListarTodosAsync();
        public Task<Produto> ListarPorIdAsync(int id);
        public Produto Atualizar(Produto produto);
        public Task RemoverAsync(int id);
        public Task SalvarAsync();
    }
}