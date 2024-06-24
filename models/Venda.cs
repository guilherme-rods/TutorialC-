using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace loja.models
{
    public class Venda
    {
        public int Id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DataVenda { get; set; }

        public string NumeroNotaFiscal { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int QuantidadeVendida { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoUnitario { get; set; }
    }
}