using System.ComponentModel.DataAnnotations;
namespace FilmesAPI.Data.DTO.Cinema
{   
    /// <summary>
    /// Cinema e Referência de endereco
    /// </summary>
   public class CreateCinemaDto
    {
        [Required(ErrorMessage = "O campo de nome é obrigatório")]
        public string Nome { get; set; }
        public int EnderecoId { get; set; }
        public int GerenteId { get; set; }
    }
}
