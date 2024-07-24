using System.ComponentModel.DataAnnotations;
using FornecedoresApi.Enum;

namespace FornecedoresApi.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public UnidadeDeMedida UnidadeDeMedida { get; set; }
    }
}