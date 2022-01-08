using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
  
    public class Filme
    {
        [Key]
        [Required]
        public int Id { get; internal set; }
        [Required(ErrorMessage = "O campo titulo é obrigatorio")]
        public string Titulo { get; set; }

        [StringLength(100, ErrorMessage = "O nome do diretor não pode exceder 100 caracteres")]
        public string Diretor { get; set; }

        public string Genero { get; set; }
        
        [Range(1, 600, ErrorMessage = "a duração deve ser no minimo 1 e no maximo 600")]
        public int Duracao { get; set; }
        public int ClassificacaoEtaria { get; set; }
        [JsonIgnore]
        public virtual List<Sessao> Sessoes { get; set; }

    }
}
