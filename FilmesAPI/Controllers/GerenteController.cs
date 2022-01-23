#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmesAPI.Data;
using FilmesAPI.Models;
using AutoMapper;
using FilmesAPI.Data.DTO.Gerente;

namespace FilmesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GerenteController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public GerenteController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadGerenteDto>>> GetGerente()
        {
            var gerentes = await _context.Gerentes.ToListAsync();
            var readGerenteDtOs = new List<ReadGerenteDto>();

            foreach (var gerente in gerentes)
            {
                readGerenteDtOs.Add(_mapper.Map<ReadGerenteDto>(gerente));
            }

            return readGerenteDtOs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadGerenteDto>> GetGerente(int id)
        {
            var gerente = await _context.Gerentes.FindAsync(id);

            if (gerente == null)
            {
                return NotFound();
            }

            ReadGerenteDto readGerenteDto = _mapper.Map<ReadGerenteDto>(gerente);

            return readGerenteDto;
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGerente(int id, UpdateGerenteDto gerenteDto)
        {
            var gerente = _context.Gerentes.Find(id);

            if (gerente == null)
            {
                return NotFound();
            }

            _mapper.Map(gerenteDto, gerente);
            _context.Entry(gerente).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GerenteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Gerente>> PostGerente(CreateGerenteDto gerenteDto)
        {
            var gerente = _mapper.Map<Gerente>(gerenteDto);
            _context.Gerentes.Add(gerente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGerente", new { id = gerente.Id }, gerente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGerente(int id)
        {
            var gerente = await _context.Gerentes.FindAsync(id);
            if (gerente == null)
            {
                return NotFound();
            }

            _context.Gerentes.Remove(gerente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GerenteExists(int id)
        {
            return _context.Gerentes.Any(e => e.Id == id);
        }
    }
}
