using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.Domain.Enums.StatusPedido;

namespace Ecommerce.Application.DTOs
{
    public class PedidoDTO
    {
        public int ClienteId { get; set; }
        public ICollection<ItemPedidoDTO> Produtos { get; set; } = new List<ItemPedidoDTO>();
    }
}