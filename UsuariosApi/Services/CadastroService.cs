using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Controllers;
using UsuariosApi.Data;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class CadastroService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser<int>> _userManager;

        public CadastroService(UserDbContext context, IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<Result> CadasdraUsuario(CreateUsuarioDto createUsuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createUsuarioDto);
            IdentityUser<int> identityUser = _mapper.Map<IdentityUser<int>>(usuario);
            var resultadoIdentity = await _userManager.CreateAsync(identityUser, createUsuarioDto.Password);
            var result = (resultadoIdentity.Succeeded) ? Result.Ok() : Result.Fail("Falha ao cadastrar usuario");
            
            return result;
        }
    }
}
