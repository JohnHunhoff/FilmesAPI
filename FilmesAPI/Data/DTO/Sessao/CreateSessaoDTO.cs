namespace FilmesAPI.Data.DTO.Sessao
{
    public class CreateSessaoDTO
    {
        public int FilmeId { get; set; }
        public int CinemaId { get; set; }
        public DateTime HorarioDeEncerramento { get; set; }
    }
}
