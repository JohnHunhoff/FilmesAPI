using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO.Cinema;
using FilmesAPI.Data.DTO.Gerente;
using FilmesAPI.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Services
{
    public class GerenteService
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public GerenteService(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadGerenteDto>> GetGerentes()
        {
            var gerentes = await _context.Gerentes.ToListAsync();
            return _mapper.Map<List<ReadGerenteDto>>(gerentes);
        }

        public async Task<ReadGerenteDto?> GetGerenteById(int id)
        {
            var gerente = await _context.Gerentes.FindAsync(id);
            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        public async Task<Result> UpdateGerente(int id, UpdateGerenteDto gerenteDto)
        {
            
            var gerente = await _context.Gerentes.FindAsync(id);
            if (gerente != null)
            {
                _mapper.Map(gerenteDto, gerente);
                _context.Entry(gerente).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            var result = (gerente == null) ? Result.Fail("Gerente não encontrado") : Result.Ok();

            return result;
        }

        public async Task<ReadGerenteDto> CreateGerente(CreateGerenteDto gerenteDto)
        {
            var gerente = _mapper.Map<Gerente>(gerenteDto);
            _context.Gerentes.Add(gerente);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadGerenteDto>(gerente);
        }

        public async Task<Result> DeleteGerente(int id)
        {
            var gerente = await _context.Gerentes.FindAsync(id);
            if (gerente != null)
            {
                _context.Gerentes.Remove(gerente); 
            }
            await _context.SaveChangesAsync();
            var result = (gerente != null) ? Result.Ok() : Result.Fail("Gerente não encontrado");
            return result;
        }
    }
}
