using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Enums
{
    public class StatusPedido
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Status
        {
            Carrinho,
            Confirmado,
            Pago,
            Enviado,
            Concluido,
            Cancelado
        }
    }
}