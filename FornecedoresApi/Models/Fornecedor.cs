using System.ComponentModel.DataAnnotations;

namespace FornecedoresApi.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cnpj { get; set; }

        [Required]
        public string Endereco { get; set; }

        [Required]
        public string Telefone { get; set; }

        public List<ProdutoFornecedor> Produtos { get; set; }
    }
}