using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Ecommerce.Domain.Enums.StatusPedido;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var todos = await _pedidoService.ListarTodosAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            var pedido = await _pedidoService.ListarPorIdAsync(id);
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] PedidoDTO pedidoDTO)
        {
            var pedido = await _pedidoService.CriarPedidoAsync(pedidoDTO);
            return CreatedAtAction(nameof(ListarPorId), new { id = pedido.Id }, pedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPedido(int id, [FromBody] Status novoStatus)
        {
            var pedidoAtualizado = await _pedidoService.AtualizarPedidoAsync(id, novoStatus);
            return Ok(pedidoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverPedido(int id)
        {
            await _pedidoService.RemoverPedidoAsync(id);
            return NoContent();
        }
    }
}