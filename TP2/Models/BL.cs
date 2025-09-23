//feito por Vinicius Pontes e Eduardo Barbosa
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CBTSWE2_TP02.Models
{
    public class BL
    {
        public int Id { get; set; }

        [Required]
        public string Numero { get; set; } = string.Empty;

        [Required]
        public string Consignee { get; set; } = string.Empty;

        [Required]
        public string Navio { get; set; } = string.Empty;

        public ICollection<Container> Containers { get; set; } = new List<Container>();
    }
}
