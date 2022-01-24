using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.DTO.Endereco;
using FilmesAPI.Models;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Services
{
    public class EnderecoService
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public EnderecoService(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadEnderecoDto>> GetEnderecos()
        {
            var enderecos = await _context.Enderecos.ToListAsync();

            return _mapper.Map<List<ReadEnderecoDto>>(enderecos);
        }

        public async Task<ReadEnderecoDto?> GetEnderecoById(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            return _mapper.Map<ReadEnderecoDto?>(endereco);
        }

        public async Task<Result> UpdateEndereco(int id, CreateEnderecoDto enderecoDto)
        {          
            var endereco = await _context.Enderecos.FindAsync(id);
            _mapper.Map(enderecoDto, endereco);
            await _context.SaveChangesAsync();
            var result = (endereco != null) ? Result.Ok() : Result.Fail("endereco não encontrado");

            return result;
        }

        public async Task<ReadEnderecoDto> CreateEndereco(CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            await _context.Enderecos.AddAsync(endereco);
            await _context.SaveChangesAsync();

            return _mapper.Map<ReadEnderecoDto>(endereco);
        }

        public async Task<Result> DeleteEndereco(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);          
            if (endereco != null) _context.Enderecos.Remove(endereco);          
            await _context.SaveChangesAsync();
            Result result = (endereco != null) ? Result.Ok() : Result.Fail("endereco não encontrado");

            return result;
        }
    }
}
