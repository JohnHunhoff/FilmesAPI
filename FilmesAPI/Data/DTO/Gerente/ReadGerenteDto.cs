using FilmesAPI.Data.DTO.Cinema;

namespace FilmesAPI.Data.DTO.Gerente
{
    public class ReadGerenteDto
    {
        public string Nome { get; set; }
        public object? Cinemas { get; set; }
    }
}
