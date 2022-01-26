using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO.Sessao;
using FilmesAPI.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Services;

public class SessaoService
{
    private readonly ApiContext _context;
    private readonly IMapper _mapper;

    public SessaoService(ApiContext context, IMapper mapper)
    {
        _context = context; 
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReadSessaoDto>> GetSessoes()
    {
        var sessoes = await _context.Sessoes.ToListAsync();
        return _mapper.Map<List<ReadSessaoDto>>(sessoes);
    }

    public async Task<ReadSessaoDto?> GetSessaoById(int id)
    {
        var sessao = await _context.Sessoes.FindAsync(id);
        return _mapper.Map<ReadSessaoDto>(sessao);
    }

    public async Task<Result> UpdateSessao(int id, UpdateSessaoDto sessaoDto)
    {
        var result = Result.Fail("Sessao não encontrada");
        var sessao = await _context.Sessoes.FindAsync(id);
        _mapper.Map(sessaoDto, sessao);
        if (sessao != null) 
        {
            _context.Entry(sessao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            result = Result.Ok();
        }
        return result;
    }

    public async Task<ReadSessaoDto> CreateSessao(CreateSessaoDto sessaoDto)
    {
        var sessao = _mapper.Map<Sessao>(sessaoDto);
        await _context.Sessoes.AddAsync(sessao);
        await _context.SaveChangesAsync();

        return _mapper.Map<ReadSessaoDto>(sessao);
    }

    public async Task<Result> DeleteSessao(int id)
    {
        var result = Result.Fail("Sessao não encontrada");
        var sessao = await _context.Sessoes.FindAsync(id);
        if (sessao != null)
        {
          _context.Sessoes.Remove(sessao);
          await _context.SaveChangesAsync();
          result = Result.Ok();      
        }   
        return result;       
    }
}