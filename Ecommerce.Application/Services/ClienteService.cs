using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Repositorys;

namespace Ecommerce.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        
        public async Task<Cliente> AdicionarClienteAsync(ClienteDTO clienteDTO)
        {
            //Regra: Nome e E-mail não podem estar vazio.
            if (string.IsNullOrWhiteSpace(clienteDTO.Nome))
            {
                throw new Exception($"O nome não pode estar vazio.");
            }

            if (string.IsNullOrWhiteSpace(clienteDTO.Email))
            {
                throw new Exception($"E-mail não pode estar vazio.");
            }

            //Regra: Endereço vazio recebe a mensagem: Endereço não informado.
            if (string.IsNullOrWhiteSpace(clienteDTO.Endereco))
            {
                clienteDTO.Endereco = "Endereço não informado.";
            }

            //Regra: Não pode ter E-mail duplicado.
            var existente = await _clienteRepository.ListarPorEmailAsync(clienteDTO.Email);
            if (existente != null)
            {
                throw new Exception($"E-mail já existe.");
            }


            var clienteNovo = new Cliente(clienteDTO.Nome, clienteDTO.Email, clienteDTO.Endereco);

            await _clienteRepository.AdicionarAsync(clienteNovo);

            await _clienteRepository.SalvarAsync();

            return clienteNovo;
        }

        public async Task ApagarClienteAsync(int id)
        {
            var cliente = await _clienteRepository.ListarPorIdAsync(id);

            if (cliente == null)
            {
                throw new Exception($"Cliente de Id {id} não existe.");
            }

            //Regra: Não apagar se houver pedidos.
            if (cliente.Pedidos.Any())
            {
                throw new Exception("Não é possível apagar cliente que possui pedidos");
            }

            await _clienteRepository.RemoverAsync(cliente.Id);

            await _clienteRepository.SalvarAsync();
        }

        public async Task<Cliente> AtualizarClienteAsync(int id, ClienteEmailDTO clienteEmailDTO)
        {
            //Regra: E-mail deve ser único.
            var outroCliente = await _clienteRepository.ListarPorEmailAsync(clienteEmailDTO.Email);
            if (outroCliente != null && outroCliente.Id != id)
            {
                throw new Exception($"E-mail já existe.");
            }

            var existente = await _clienteRepository.ListarPorIdAsync(id);

            //Regra: Validar o Id
            if (existente == null)
            {
                throw new Exception($"Cliente de Id {id} não encontrado.");
            }

            existente.Email = clienteEmailDTO.Email;
            existente.Endereco = clienteEmailDTO.Endereco;

            _clienteRepository.Atualizar(existente);
            await _clienteRepository.SalvarAsync();

            return existente;
        }

        public async Task<Cliente> ListarPorIdAsync(int id)
        {
            var cliente = await _clienteRepository.ListarPorIdAsync(id);

            if (cliente == null)
            {
                throw new Exception($"Cliente de Id {id} não encontrado.");
            }

            return cliente;
        }

        public async Task<List<Cliente>> ListarPorNomeAsync(string nomeParcial)
        {
            var listar = await _clienteRepository.ListarPorNomeAsync(nomeParcial);
            return listar;
        }

        public async Task<List<Cliente>> ListarTodosAsync()
        {
            var todos = await _clienteRepository.ListarTodosAsync();
            return todos;
        }
    }
}