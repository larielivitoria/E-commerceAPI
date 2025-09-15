using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.Domain.Enums;
using static Ecommerce.Domain.Enums.StatusPedido;

namespace Ecommerce.Domain.Entities
{
    public class Pedido
    {
        public Pedido(int clienteId)
        {
            ClienteId = clienteId;
        }
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now;
        public Status Status { get; set; } = Status.Carrinho;
        public ICollection<ItemPedido> Produtos { get; set; } = new List<ItemPedido>();
        public decimal Total => Produtos.Sum(i => i.SubTotal);
    }
}