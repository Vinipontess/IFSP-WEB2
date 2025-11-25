using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    public class Tarefa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título da tarefa é obrigatório")]
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        [Required(ErrorMessage = "A tarefa deve ser cadastrada como concluida ou não concluida")]
        public bool Concluida { get; set; }
        [Required(ErrorMessage = "A data da criação é obrigatória")]
        public DateTime DataCriacao { get; set; }
    }
}
