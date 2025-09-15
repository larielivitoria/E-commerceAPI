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
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly EcommerceContext _context;
        public ProdutoRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<Produto> AdicionarAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            return produto;
        }

        public Produto Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
            return produto;
        }

        public async Task<Produto> ListarPorIdAsync(int id)
        {
            var listar = await _context.Produtos.FindAsync(id);
            if (listar == null)
            {
                throw new Exception($"Produto de Id {id} não existe");
            }
            return listar;
        }

        public Task<List<Produto>> ListarTodosAsync()
        {
            var listar = _context.Produtos.ToListAsync();
            return listar;
        }

        public async Task RemoverAsync(int id)
        {
            var existe = await _context.Produtos.FindAsync(id);
            if (existe == null)
            {
                throw new Exception($"Produto de Id {id} não existe.");
            }
            _context.Produtos.Remove(existe);
        }

        public async Task SalvarAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}