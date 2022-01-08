using System.ComponentModel.DataAnnotations;
using FilmesAPI.Models;
namespace FilmesAPI.Data.DTO.Cinema
{
    public class ReadCinemaDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo de nome é obrigatório")]
        public string Nome { get; set; }
        public Models.Endereco Endereco { get; set; }
        public Models.Gerente Gerente { get; set; }
    }
}
