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
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<Produto> AdicionarProdutoAsync(ProdutoDTO produtoDTO)
        {
            //Regra: Nome não pode estar vazio.
            if (string.IsNullOrWhiteSpace(produtoDTO.Nome))
            {
                throw new Exception("Nome não pode estar vazio.");
            }

            //Regra: Descrição recebe mensagem se estiver vazia.
            if (string.IsNullOrWhiteSpace(produtoDTO.Descricao))
            {
                produtoDTO.Descricao = "Descrição não informada.";
            }

            //Regra: Preço não pode ser zero.
            if (produtoDTO.Preco <= 0)
            {
                throw new Exception("Preço deve ser maior que zero.");
            }

            //Regra: Estoque não pode ser zero.
            if (produtoDTO.Estoque <= 0)
            {
                throw new Exception("Estoque deve ser maior que zero.");
            }

            var produto = new Produto(produtoDTO.Nome, produtoDTO.Descricao, produtoDTO.Preco, produtoDTO.Estoque);

            await _produtoRepository.AdicionarAsync(produto);

            await _produtoRepository.SalvarAsync();

            return produto;
        }

        public async Task<Produto> AtualizarProdutoAsync(int id, ProdutoAtualizarDTO produtoAtualizarDTO)
        {
            var existente = await _produtoRepository.ListarPorIdAsync(id);

            //Regra: Validar Id.
            if (existente == null)
            {
                throw new Exception($"Produto de Id {id} não encontrado.");
            }

            //Regra: Estoque não pode ser zero.
            if (produtoAtualizarDTO.Estoque <= 0)
            {
                throw new Exception("Estoque deve ser maior que zero.");
            }

            //Regra: Preço não pode ser zero.
            if (produtoAtualizarDTO.Preco <= 0)
            {
                throw new Exception("Preço deve ser maior que zero.");
            }

            existente.Estoque = produtoAtualizarDTO.Estoque;
            existente.Preco = produtoAtualizarDTO.Preco;

            _produtoRepository.Atualizar(existente);
            await _produtoRepository.SalvarAsync();

            return existente;
        }

        public async Task<Produto> ListarPorIdAsync(int id)
        {
            var produto = await _produtoRepository.ListarPorIdAsync(id);
            return produto;
        }

        public async Task<List<Produto>> ListarTodosAsync()
        {
            var todos = await _produtoRepository.ListarTodosAsync();
            return todos;
        }

        public async Task RemoverProdutoAsync(int id)
        {
            await _produtoRepository.RemoverAsync(id);
            await _produtoRepository.SalvarAsync();
        }
    }
}