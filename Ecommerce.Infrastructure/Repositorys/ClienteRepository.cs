using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Repositorys;
using Ecommerce.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositorys
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly EcommerceContext _context;
        public ClienteRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<Cliente> AdicionarAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            return cliente;
        }

        public Cliente Atualizar(Cliente cliente)
        {   
            _context.Clientes.Update(cliente);
            return cliente;
        }

        public async Task<Cliente?> ListarPorIdAsync(int id)
        {
            var cliente = await _context.Clientes.Include(c => c.Pedidos)
                                                .FirstOrDefaultAsync(c => c.Id == id);
                return cliente;
        }

        public async Task<List<Cliente>> ListarPorNomeAsync(string nomeParcial)
        {
            var listar = await _context.Clientes.Where(c => c.Nome.ToLower().Contains(nomeParcial.ToLower())).ToListAsync();
            return listar;
        }

        public async Task<Cliente?> ListarPorEmailAsync(string email)
        {
            var listar = await _context.Clientes.FirstOrDefaultAsync(c => c.Email == email);
            return listar;
        }

        public Task<List<Cliente>> ListarTodosAsync()
        {
            var listar = _context.Clientes.Include(c => c.Pedidos).ThenInclude(p => p.Produtos).ToListAsync();
            return listar;
        }

        public async Task RemoverAsync(int id)
        {
            var existe = await _context.Clientes.FindAsync(id);
            if (existe == null)
            {
                throw new Exception($"Cliente de Id {id} não existe.");
            }
            _context.Clientes.Remove(existe);
        }

        public async Task SalvarAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}