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
using FilmesAPI.Data.DTO.Cinema;
using FilmesAPI.Data.DTO.Gerente;
using FilmesAPI.Migrations;
using FilmesAPI.Services;
using FluentResults;

namespace FilmesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GerenteController : ControllerBase
    {
        private readonly GerenteService _gerenteService;

        public GerenteController(GerenteService gerenteService)
        {
            _gerenteService = gerenteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadGerenteDto>>> GetGerente()
        {
            IEnumerable<ReadGerenteDto> readGerenteDto = await _gerenteService.GetGerentes();
            return Ok(readGerenteDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadGerenteDto>> GetGerente(int id)
        {
            var readGerenteDto = await _gerenteService.GetGerenteById(id);
            ActionResult response = (readGerenteDto != null) ? Ok(readGerenteDto) : NotFound();
            
            return response;
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGerente(int id, UpdateGerenteDto gerenteDto)
        {
            Result result = await _gerenteService.UpdateGerente(id, gerenteDto);
            ActionResult response = (result.IsSuccess) ? NoContent() : NotFound();
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<ReadGerenteDto>> PostGerente(CreateGerenteDto gerenteDto)
        {
            ReadGerenteDto readGerenteDto = await _gerenteService.CreateGerente(gerenteDto);
            return CreatedAtAction("GetGerente", new { id = readGerenteDto.Id }, readGerenteDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGerente(int id)
        {
            Result result = await _gerenteService.DeleteGerente(id);
            ActionResult response = (result.IsSuccess) ? NoContent() : NotFound();
            return response;
        }
    }
}
