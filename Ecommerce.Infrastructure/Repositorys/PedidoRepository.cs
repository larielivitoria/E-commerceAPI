using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Repositorys;
using Ecommerce.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositorys
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly EcommerceContext _context;
        public PedidoRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<Pedido> AdicionarAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            return pedido;
        }

        public Pedido Atualizar(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            return pedido;
        }

        public async Task<Pedido> ListarPorIdAsync(int id)
        {
            var listar = await _context.Pedidos.Include(p => p.Produtos)
                                                .FirstOrDefaultAsync(p => p.Id == id);

            if (listar == null)
            {
                throw new Exception($"Pedido de Id {id} não existe.");
            }

            return listar;
        }

        public Task<List<Pedido>> ListarTodosAsync()
        {
            return _context.Pedidos.Include(p => p.Produtos).ToListAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var existe = await _context.Pedidos.FindAsync(id);
            if (existe == null)
            {
                throw new Exception($"Pedido de Id {id} não existe.");
            }

            _context.Pedidos.Remove(existe);
        }

        public async Task SalvarAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}