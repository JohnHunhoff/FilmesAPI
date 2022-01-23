using System.ComponentModel.DataAnnotations;
using FilmesAPI.Models;
namespace FilmesAPI.Data.DTO.Filme
{
    public class ReadFilmeDto
    {
        public int Id { get;  set; }
        [Required(ErrorMessage = "O campo titulo é obrigatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo Diretor é obrigatorio")]
        public string Diretor { get; set; }

        public string Genero { get; set; }

        [Range(1, 600, ErrorMessage = "a duração deve ser no minimo 1 e no maximo 600")]
        public int Duracao { get; set; }

        public DateTime DataConsulta { get; set; }

        public virtual List<Models.Sessao> Sessoes { get; set; }
        

        public ReadFilmeDto()
        {
            DataConsulta = DateTime.Now;
        }
    }
}
