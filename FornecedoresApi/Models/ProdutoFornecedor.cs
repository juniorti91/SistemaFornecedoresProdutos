using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FornecedoresApi.Models
{
    public class ProdutoFornecedor
    {
        [Key]
        public int ProdutoFornecedorId { get; set; }
        public int ProdutoId { get; set; }        

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorCompra { get; set; }
        
        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }
    }
}