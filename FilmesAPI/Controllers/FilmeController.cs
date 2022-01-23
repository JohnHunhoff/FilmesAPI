using FilmesAPI.Data;
using FilmesAPI.Data.DTO.Filme;
using FilmesAPI.Models;
using FilmesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeService _filmeService;
        public FilmeController(FilmeService filmeService)
        { 
            _filmeService = filmeService;
        }


        [HttpPost]
        public async Task<ActionResult<ReadFilmeDto>> PostFilme([FromBody]CreateFilmeDto filmeDto)
        {
            var readFilmeDto = await _filmeService.CreateFilme(filmeDto);
            
            return CreatedAtAction(nameof(GetFilmes), new {id = readFilmeDto.Id} , readFilmeDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadFilmeDto>>> GetFilmes([FromQuery] int? classificacaoEtaria = null) 
        {
            var readFilmeDto = await _filmeService.GetFilmes(classificacaoEtaria);
            if(readFilmeDto != null) return Ok(readFilmeDto);
            return NotFound(); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadFilmeDto>> GetFilmes(int id)
        {
            var readFilmeDto = await _filmeService.GetFilmeById(id);
            if(readFilmeDto != null) return Ok(readFilmeDto);
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutFilme(int id, [FromBody] UpdateFilmeDto filmeNovoDto)
        {
            Result result = await _filmeService.UpdateFilme(id, filmeNovoDto);

            if (result.IsFailed) return NotFound();                     
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFilme(int id)
        {
            Result result = await _filmeService.DeleteFilme(id);
            if (result.IsFailed) return NotFound();
            return NoContent();
        }
    }
}
