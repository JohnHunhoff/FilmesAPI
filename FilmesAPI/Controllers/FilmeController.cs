using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class FilmeController : ControllerBase
    {

        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost]
        public IActionResult AdicionaFilme([FromBody]CreateFilmeDTO filmeDTO)
        {
            Filme filme = _mapper.Map<Filme>(filmeDTO);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmes), new {id = filme.Id} , filme);
        }

        [HttpGet]
        public IEnumerable<Filme> RecuperaFilmes() 
        {
            return _context.Filmes; 
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmes(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme != null)
            {
                ReadFilmeDTO filmeDTO = _mapper.Map<ReadFilmeDTO>(filme);

                return Ok(filmeDTO);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDTO filmeNovoDTO)
        {
            var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme == null)
            {
                return NotFound();
            }

            _mapper.Map(filmeNovoDTO, filme);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            var filme =_context.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme == null)
            {
                return NotFound();
            }
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
