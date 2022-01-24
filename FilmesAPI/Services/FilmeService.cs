using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO.Filme;
using FilmesAPI.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Services
{
    public class FilmeService
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public FilmeService(ApiContext apiContext, IMapper mapper)
        {
            _context = apiContext;
            _mapper = mapper;
        }

        public async Task<ReadFilmeDto> CreateFilme(CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);
            await _context.Filmes.AddAsync(filme);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<ReadFilmeDto>(filme);
        }

        public async Task<IEnumerable<ReadFilmeDto>?> GetFilmes(int? classificacaoEtaria)
        {
            List<Filme> filmes;
            if (classificacaoEtaria == null)
            {
                filmes = await _context.Filmes.ToListAsync();
            }
            else
            {
                filmes = await _context.Filmes
                    .Where(f => f.ClassificacaoEtaria <= classificacaoEtaria)
                    .ToListAsync();
            }

            if (filmes != null)
            {
                List<ReadFilmeDto> readFilmeDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return readFilmeDto;
            }

            return null;
        }

        public async Task<ReadFilmeDto>? GetFilmeById(int id)
        {
            ReadFilmeDto? readFilmeDto = null;
            var filme = await _context.Filmes.FirstOrDefaultAsync(f => f.Id == id);
            if (filme != null)
            {
                readFilmeDto = _mapper.Map<ReadFilmeDto>(filme);
            }
            return readFilmeDto;
        }

        public async Task<Result> UpdateFilme(int id, UpdateFilmeDto filmeNovoDto)
        {
            Result result = Result.Fail("Filme não encontrado");
            var filme = await _context.Filmes.FirstOrDefaultAsync(f => f.Id == id);
            if (filme != null)
            {
                _mapper.Map(filmeNovoDto, filme);
                await _context.SaveChangesAsync();
                result = Result.Ok();
            }
           
            return result;
        }

        public async Task<Result> DeleteFilme(int id)
        {
            Result result = Result.Fail("Filme não encontrado");
            var filme = await _context.Filmes.FirstOrDefaultAsync(f => f.Id == id);
            if (filme != null)
            {
                _context.Entry(filme).State = EntityState.Deleted;               
                await _context.SaveChangesAsync();
                result = Result.Ok();
            }
            return result;
        }
    }

}
