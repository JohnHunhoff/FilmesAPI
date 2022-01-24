using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO.Cinema;
using FilmesAPI.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Services;

public class CinemaService
{
    private readonly ApiContext _context;
    private readonly IMapper _mapper;

    public CinemaService(ApiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ReadCinemaDto> CreateCinema(CreateCinemaDto createCinemaDto)
    {
        Cinema? cinema = _mapper.Map<Cinema>(createCinemaDto);
        await _context.Cinemas.AddAsync(cinema);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReadCinemaDto>(cinema);
    }

    public async Task<IEnumerable<ReadCinemaDto>?> GetCinemas()
    {
        var cinemas = await _context.Cinemas.ToListAsync();
        var readCinemaDtos = new List<ReadCinemaDto>();

        foreach (var cinema in cinemas)
        {
            readCinemaDtos.Add(_mapper.Map<ReadCinemaDto>(cinema));
        }

        return readCinemaDtos;
    }

    public async Task<ReadCinemaDto?> GetCinemaById(int id)
    {
        ReadCinemaDto? readCinemaDto = null;
        var cinema = await _context.Cinemas
            .FirstOrDefaultAsync(cinema => cinema.Id == id);
        if (cinema != null)
        {
            readCinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
        }
        return readCinemaDto;
    }

    public async Task<Result> UpdateCinema(int id, UpdateCinemaDto updateCinemaDto)
    {
        Cinema? cinema = await _context.Cinemas.FirstOrDefaultAsync(c => c.Id == id);     
        _mapper.Map(updateCinemaDto, cinema);
        await _context.SaveChangesAsync();
        Result result = (cinema != null) ? Result.Ok() : Result.Fail("Cinema não encontrado");

        return result;
    }

    public async Task<Result> DeleteCinema(int id)
    {
        Cinema? cinema = await _context.Cinemas.FirstOrDefaultAsync(cinema => cinema.Id == id);
        if (cinema != null) _context.Entry(cinema).State = EntityState.Deleted;       
        await _context.SaveChangesAsync();
        Result result = (cinema != null) ? Result.Ok() : Result.Fail("Cinema não Encontrado");

        return result;
    }
}