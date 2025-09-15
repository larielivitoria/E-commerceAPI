using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Repositorys;
using Ecommerce.Domain.Validators;
using static Ecommerce.Domain.Enums.StatusPedido;

namespace Ecommerce.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PedidoService(IPedidoRepository pedidoRepository, IClienteRepository clienteRepository, IProdutoRepository produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<Pedido> AtualizarPedidoAsync(int id, Status novoStatus)
        {
            var pedido = await _pedidoRepository.ListarPorIdAsync(id);
            if (pedido == null)
            {
                throw new Exception($"Pedido de Id {id} não encontrado.");
            }

            //Regra: Não pode atualizar se já estiver cancelado.
            if (pedido.Status == Status.Cancelado)
            {
                throw new Exception("Pedido cancelado não pode ser atualizado.");
            }

            //Regra: Status segue uma ordem lógica.
            if (!PedidoStatusValidator.PodeMudarStatus(pedido.Status, novoStatus))
            {
                throw new Exception($"Não é possível alterar de {pedido.Status} para {novoStatus}.");
            }

            pedido.Status = novoStatus;

            _pedidoRepository.Atualizar(pedido);
            await _pedidoRepository.SalvarAsync();

            return pedido;
        }

        public async Task<Pedido> CriarPedidoAsync(PedidoDTO pedidoDTO)
        {
            //Regra: Cliente deve ser válido.
            var cliente = await _clienteRepository.ListarPorIdAsync(pedidoDTO.ClienteId);
            if (cliente == null)
            {
                throw new Exception($"Cliente de Id {pedidoDTO.ClienteId} não encontrado.");
            }

            //Regra: Id e quantidade não pode ser zero.
            foreach (var produto in pedidoDTO.Produtos)
            {
                if (produto.ProdutoId <= 0)
                {
                    throw new Exception("O Produto deve ter um Id válido.");
                }
                if (produto.Quantidade <= 0)
                {
                    throw new Exception($"Quantidade deve ser maior que zero.");
                }
            }

            var pedidoNovo = new Pedido(pedidoDTO.ClienteId);

            foreach (var itemDto in pedidoDTO.Produtos)
            {
                //Regra: verificar se o produto existe.
                var produto = await _produtoRepository.ListarPorIdAsync(itemDto.ProdutoId);
                if (produto == null)
                {
                    throw new Exception($"Produto de Id {itemDto.ProdutoId} não encontrado.");
                }

                //Regra: Verificar estoque disponível.
                if (produto.Estoque < itemDto.Quantidade)
                {
                    throw new Exception($"Produto {produto.Nome} não tem estoque suficiente.");
                }

                //Atualiza estoque.
                produto.Estoque -= itemDto.Quantidade;
                _produtoRepository.Atualizar(produto);

                //Cria ItemPedido.
                var itemPedido = new ItemPedido(
                    pedido: pedidoNovo,
                    produtoId: produto.Id,
                    quantidade: itemDto.Quantidade,
                    precoUnitario: produto.Preco
                );

                pedidoNovo.Produtos.Add(itemPedido);
            }

            await _produtoRepository.SalvarAsync();
            await _pedidoRepository.AdicionarAsync(pedidoNovo);
            await _pedidoRepository.SalvarAsync();

            return pedidoNovo;
        }

        public async Task<Pedido> ListarPorIdAsync(int id)
        {
            var pedido = await _pedidoRepository.ListarPorIdAsync(id);
            return pedido;
        }

        public async Task<List<Pedido>> ListarTodosAsync()
        {
            var todos = await _pedidoRepository.ListarTodosAsync();
            return todos;
        }

        public async Task RemoverPedidoAsync(int id)
        {
            await _pedidoRepository.RemoverAsync(id);
            await _pedidoRepository.SalvarAsync();
        }
    }
}