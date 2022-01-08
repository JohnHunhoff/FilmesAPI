using AutoMapper;
using FilmesAPI.Data.DTO.Gerente;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class GerenteProfile : Profile
    {
        public GerenteProfile()
        {
            CreateMap<CreateGerenteDTO, Gerente>();
            CreateMap<UpdateGerenteDTO, Gerente>();
            CreateMap<Gerente, ReadGerenteDTO>()
                .ForMember(cinema => cinema.Cinemas, opt => opt
                .MapFrom(gerente => gerente.Cinemas.Select(
                    c => new {c.Nome, c.Endereco.Logradouro, c.Endereco.Bairro} )));
        }
    }
}
