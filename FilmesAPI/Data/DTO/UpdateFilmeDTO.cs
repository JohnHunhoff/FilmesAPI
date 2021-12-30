using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTO
{
    public class UpdateFilmeDTO
    {
        [Required(ErrorMessage = "O campo titulo é obrigatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo Diretor é obrigatorio")]
        public string Diretor { get; set; }

        public string Genero { get; set; }

        [Range(1, 600, ErrorMessage = "a duração deve ser no minimo 1 e no maximo 600")]
        public int Duracao { get; set; }
    }
}
