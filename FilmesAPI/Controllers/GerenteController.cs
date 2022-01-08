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
        public async Task<ActionResult<IEnumerable<ReadGerenteDTO>>> GetGerente()
        {
            var gerentes = await _context.Gerentes.ToListAsync();
            var readGerenteDTOs = new List<ReadGerenteDTO>();

            foreach (var gerente in gerentes)
            {
                readGerenteDTOs.Add(_mapper.Map<ReadGerenteDTO>(gerente));
            }

            return readGerenteDTOs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadGerenteDTO>> GetGerente(int id)
        {
            var gerente = await _context.Gerentes.FindAsync(id);

            if (gerente == null)
            {
                return NotFound();
            }

            ReadGerenteDTO readGerenteDTO = _mapper.Map<ReadGerenteDTO>(gerente);

            return readGerenteDTO;
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGerente(int id, UpdateGerenteDTO gerenteDTO)
        {
            var gerente = _context.Gerentes.Find(id);

            if (gerente == null)
            {
                return NotFound();
            }

            _mapper.Map(gerenteDTO, gerente);
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
        public async Task<ActionResult<Gerente>> PostGerente(CreateGerenteDTO gerenteDTO)
        {
            var gerente = _mapper.Map<Gerente>(gerenteDTO);
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
