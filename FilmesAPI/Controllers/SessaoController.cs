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
using FilmesAPI.Data.DTO.Sessao;
using FilmesAPI.Services;
using FluentResults;

namespace FilmesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SessaoController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        private readonly SessaoService _sessaoService;

        public SessaoController(ApiContext context, IMapper mapper, SessaoService sessaoService)
        {
            _context = context;
            _mapper = mapper;
            _sessaoService = sessaoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadSessaoDto>>> GetSessoes()
        {
            var readSessaoDto = await _sessaoService.GetSessoes();
            return Ok(readSessaoDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadSessaoDto>> GetSessao(int id)
        {
            ReadSessaoDto readSessaoDto = await _sessaoService.GetSessaoById(id);
            ActionResult response = (readSessaoDto != null) ? Ok(readSessaoDto) : NotFound();
            
            return response;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutSessao(int id, UpdateSessaoDto sessaoDto)
        {
            Result result = await _sessaoService.UpdateSessao(id, sessaoDto);
            ActionResult response = (result.IsSuccess) ? NoContent() : NotFound();

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<Sessao>> PostSessao(CreateSessaoDto sessaoDto)
        {
            ReadSessaoDto readSessaoDto = await _sessaoService.CreateSessao(sessaoDto);
            

            return CreatedAtAction("GetSessao", new { id = readSessaoDto.Id }, readSessaoDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessao(int id)
        {
            Result result = await _sessaoService.DeleteSessao(id);
            ActionResult response = (result.IsSuccess) ? NoContent() : NotFound();
            return response;
        }
    }
}
