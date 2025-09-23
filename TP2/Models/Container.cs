//feito por Vinicius Pontes e Eduardo Barbosa
using System.ComponentModel.DataAnnotations;

namespace CBTSWE2_TP02.Models
{
    //Desenvolvido por Laysa Bernardes e Lucas Lopes
    public class Container
    {
        public int Id { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O número do container deve ter 11 caracteres.")]
        public string Numero { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(Dry|Reefer)$", ErrorMessage = "O tipo deve ser 'Dry' ou 'Reefer'.")]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        [Range(20, 40, ErrorMessage = "O tamanho deve ser 20 ou 40.")]
        public int Tamanho { get; set; }

        [Required]
        public int BLId { get; set; }
        public BL BL { get; set; } = null!;
    }
}
    