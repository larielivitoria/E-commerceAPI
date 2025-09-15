using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var todos = await _produtoService.ListarTodosAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            var produto = await _produtoService.ListarPorIdAsync(id);
            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarProduto([FromBody] ProdutoDTO produtoDTO)
        {
            var produto = await _produtoService.AdicionarProdutoAsync(produtoDTO);
            return CreatedAtAction(nameof(ListarPorId), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] ProdutoAtualizarDTO produtoAtualizarDTO)
        {
            var produtoAtualizado = await _produtoService.AtualizarProdutoAsync(id, produtoAtualizarDTO);
            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverProduto(int id)
        {
            await _produtoService.RemoverProdutoAsync(id);
            return NoContent();
        }
    }
}