using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        public async Task<ActionResult> CreateUsuario(CreateUsuarioDto createUsuarioDto)
        {
            Result result = await _cadastroService.CadasdraUsuario(createUsuarioDto);
            ActionResult response = (result.IsSuccess) ? Ok() : StatusCode(500);
            return response;
        }
    }
}
