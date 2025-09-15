using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Repositorys
{
    public interface IPedidoRepository
    {
        public Task<Pedido> AdicionarAsync(Pedido pedido);
        public Task<List<Pedido>> ListarTodosAsync();
        public Task<Pedido> ListarPorIdAsync(int id);
        public Pedido Atualizar(Pedido pedido);
        public Task RemoverAsync(int id);
        public Task SalvarAsync();
    }
}