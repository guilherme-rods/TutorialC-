using System.ComponentModel.DataAnnotations.Schema;

namespace loja.models{
    public class Produto{
        public int Id { get; set; }
        public String Nome { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public Decimal Preco { get; set; }

        public String Fornecedor { get; set; }

    }
}