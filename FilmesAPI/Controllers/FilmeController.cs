using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO.Filme;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class FilmeController : ControllerBase
    {

        private Data.ApiContext _context;
        private IMapper _mapper;

        public FilmeController(ApiContext context, IMapper mapper)
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
            return CreatedAtAction(nameof(GetFilme), new {id = filme.Id} , filme);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadFilmeDTO>>> GetFilmes([FromQuery] int? classificacaoEtaria = null) 
        {
            var filmes =  _context.Filmes.Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria).ToList();
            if (filmes != null) 
            { 
                List<ReadFilmeDTO> readDTO = _mapper.Map<List<ReadFilmeDTO>>(filmes);
                return Ok(readDTO);
            }
            return NotFound(); 
        }

        [HttpGet("{id}")]
        public IActionResult GetFilme(int id)
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
        public IActionResult PutFilme(int id, [FromBody] UpdateFilmeDTO filmeNovoDTO)
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
