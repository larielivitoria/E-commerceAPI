using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Repositorys
{
    public interface IClienteRepository
    {
        public Task<Cliente> AdicionarAsync(Cliente cliente);
        public Task<List<Cliente>> ListarTodosAsync();
        public Task<Cliente> ListarPorIdAsync(int id);
        public Task<List<Cliente>> ListarPorNomeAsync(string nomeParcial);
        public Task<Cliente> ListarPorEmailAsync(string email);
        public Cliente Atualizar(Cliente cliente);
        public Task RemoverAsync(int id);
        public Task SalvarAsync();
    }
}