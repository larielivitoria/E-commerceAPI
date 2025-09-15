using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var todos = await _clienteService.ListarTodosAsync();
            return Ok(todos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            var cliente = await _clienteService.ListarPorIdAsync(id);
            return Ok(cliente);
        }

        [HttpGet("por-nome/{nomeParcial}")]
        public async Task<IActionResult> ListarPorNome(string nomeParcial)
        {
            var cliente = await _clienteService.ListarPorNomeAsync(nomeParcial);
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCliente([FromBody] ClienteDTO clienteDTO)
        {
            var add = await _clienteService.AdicionarClienteAsync(clienteDTO);
            return CreatedAtAction(nameof(ListarPorId), new { id = add.Id }, add);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente(int id, [FromBody] ClienteEmailDTO clienteEmailDTO)
        {
            var clienteAtualizado = await _clienteService.AtualizarClienteAsync(id, clienteEmailDTO);
            return Ok(clienteAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ApagarCliente(int id)
        {
            await _clienteService.ApagarClienteAsync(id);
            return NoContent();
        }
    }
}