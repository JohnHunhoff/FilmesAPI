using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult> UserLogin(LoginRequest loginRequest)
        {
            var result = await _loginService.UserLogin(loginRequest);
            ActionResult response = result.IsSuccess ? Ok(result.Successes) : Unauthorized();
            return response;
        }
    }
}
