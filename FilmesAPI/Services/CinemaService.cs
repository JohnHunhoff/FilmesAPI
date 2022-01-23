using AutoMapper;
using FilmesAPI.Data;

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
    
    
}