using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces
{
    public interface IClienteService
    {
        public Task<Cliente> AdicionarClienteAsync(ClienteDTO clienteDTO);
        public Task<List<Cliente>> ListarTodosAsync();
        public Task<Cliente> ListarPorIdAsync(int id);
        public Task<List<Cliente>> ListarPorNomeAsync(string nomeParcial);
        public Task<Cliente> AtualizarClienteAsync(int id, ClienteEmailDTO clienteEmailDTO);
        public Task ApagarClienteAsync(int id);
    }
}