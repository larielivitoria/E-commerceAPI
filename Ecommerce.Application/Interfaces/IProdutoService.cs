using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces
{
    public interface IProdutoService
    {
        public Task<Produto> AdicionarProdutoAsync(ProdutoDTO produtoDTO);
        public Task<List<Produto>> ListarTodosAsync();
        public Task<Produto> ListarPorIdAsync(int id);
        public Task<Produto> AtualizarProdutoAsync(int id, ProdutoAtualizarDTO produtoAtualizarDTO);
        public Task RemoverProdutoAsync(int id);
    }
}