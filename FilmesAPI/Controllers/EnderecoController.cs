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
using FilmesAPI.Data.DTO.Endereco;
using AutoMapper;
using FilmesAPI.Services;
using Castle.Core.Internal;
using FluentResults;

namespace FilmesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoService _enderecoService;

        public EnderecoController(EnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadEnderecoDto>>> GetEndereco()
        {
            var readEnderecoDto = await _enderecoService.GetEnderecos();          
            return Ok(readEnderecoDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadEnderecoDto>> GetEndereco(int id)
        {
            var readEnderecoDto = await _enderecoService.GetEnderecoById(id);
            ActionResult response = (readEnderecoDto != null) ? Ok(readEnderecoDto) : NotFound();
            return response;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndereco(int id, CreateEnderecoDto enderecoDto)
        {          
            Result result = await _enderecoService.UpdateEndereco(id, enderecoDto);
            ActionResult response = (result.IsFailed) ? NotFound() : NoContent();

            return response;
        }
       
        [HttpPost]
        public async Task<ActionResult<ReadEnderecoDto>> PostEndereco([FromBody]CreateEnderecoDto enderecoDto)
        {
            ReadEnderecoDto readEnderecoDto = await _enderecoService.CreateEndereco(enderecoDto);        
            return CreatedAtAction("GetEndereco", new { id = readEnderecoDto.Id }, readEnderecoDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            Result result = await _enderecoService.DeleteEndereco(id);
            ActionResult response = (result.IsFailed) ? NotFound() : NoContent();

            return response;
        }      
    }
}
