using AutoMapper;
using Castle.Core.Internal;
using FilmesAPI.Data.DTO.Cinema;
using FilmesAPI.Data;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentResults;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        private readonly CinemaService _cinemaService;

        public CinemaController(ApiContext context, IMapper mapper, CinemaService cinemaService)
        {
            _context = context;
            _mapper = mapper;
            _cinemaService = cinemaService;
        }


        [HttpPost]
        public async Task<ActionResult<ReadCinemaDto>> PostCinema([FromBody] CreateCinemaDto createCinemaDto)
        {
            ReadCinemaDto readCinemaDto = await _cinemaService.CreateCinema(createCinemaDto);
            return CreatedAtAction(nameof(GetCinema), new { readCinemaDto.Id }, readCinemaDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadCinemaDto>>> GetCinema()
        {
            var readCinemaDto = await _cinemaService.GetCinemas();
            ActionResult response = (!readCinemaDto.IsNullOrEmpty()) ? Ok(readCinemaDto) : NotFound();
            
            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCinemaDto>> GetCinema(int id)
        {
            var readCinemaDto = await _cinemaService.GetCinemaById(id);
            ActionResult response = (readCinemaDto != null) ? Ok(readCinemaDto) : NotFound(); 
            
            return response;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCinema(int id, [FromBody] UpdateCinemaDto updateCinemaDto)
        {
            Result result = await _cinemaService.UpdateCinema(id, updateCinemaDto);
            ActionResult response = (!result.IsFailed) ? NoContent() : NotFound();
            
            return response;
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCinema(int id)
        {
            Result result = await _cinemaService.DeleteCinema(id);
            ActionResult response = (!result.IsFailed) ? NoContent() : NotFound();
            return response;
        }

    }
}
