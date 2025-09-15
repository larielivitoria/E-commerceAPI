using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.DTOs
{
    public class ProdutoAtualizarDTO
    {
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
    }
}