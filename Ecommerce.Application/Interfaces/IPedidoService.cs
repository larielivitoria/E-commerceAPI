using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;
using static Ecommerce.Domain.Enums.StatusPedido;

namespace Ecommerce.Application.Interfaces
{
    public interface IPedidoService
    {
        public Task<Pedido> CriarPedidoAsync(PedidoDTO pedidoDTO);
        public Task<List<Pedido>> ListarTodosAsync();
        public Task<Pedido> ListarPorIdAsync(int id);
        public Task<Pedido> AtualizarPedidoAsync(int id, Status novoStatus);
        public Task RemoverPedidoAsync(int id);
    }
}