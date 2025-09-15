using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Enums;
using static Ecommerce.Domain.Enums.StatusPedido;

namespace Ecommerce.Domain.Validators
{
    public static class PedidoStatusValidator
    {
        public static bool PodeMudarStatus(Status statusAtual, Status novoStatus)
        {
            return statusAtual switch
            {
                Status.Carrinho => novoStatus == Status.Confirmado || novoStatus == Status.Cancelado,
                Status.Confirmado => novoStatus == Status.Pago || novoStatus == Status.Cancelado,
                Status.Pago => novoStatus == Status.Enviado || novoStatus == Status.Cancelado,
                Status.Enviado => novoStatus == Status.Concluido || novoStatus == Status.Cancelado,
                _ => false
            };
        }
    }
}