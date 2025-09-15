using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class ItemPedido
    {
        public ItemPedido()
        {
            
        }
        public ItemPedido(Pedido pedido, int produtoId, int quantidade, decimal precoUnitario)
        {
            Pedido = pedido;
            ProdutoId = produtoId;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }
        public int Id { get; set; }
        public int PedidoId { get; set; }
        
        [JsonIgnore]
        public Pedido Pedido { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal SubTotal => Quantidade * PrecoUnitario;
    }
}