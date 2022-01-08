﻿using FilmesAPI.Models;
namespace FilmesAPI.Data.DTO.Sessao
{
    public class ReadSessaoDTO
    {
        public int Id { get; set; }
        public DateTime HorarioDeEncerramento { get; set; }
        public DateTime HorarioDeInicio { get; set; }
        public Models.Cinema Cinema { get; set; }
        public Models.Filme Filme { get; set; }
    }
}
