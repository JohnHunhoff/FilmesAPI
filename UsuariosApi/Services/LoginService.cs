using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Controllers;

namespace UsuariosApi.Services
{
    public class LoginService
    {
        private readonly SignInManager<IdentityUser<int>> _signManager;
        private readonly TokenService _tokenService;
        
        public LoginService(SignInManager<IdentityUser<int>> signManager, 
            TokenService tokenService)
        {
            _signManager = signManager;
            _tokenService = tokenService;
        }

        public async Task<Result> UserLogin(LoginRequest loginRequest)
        {
            var resultadoIdentity = await _signManager
                .PasswordSignInAsync(loginRequest.Username, loginRequest.Password, false, false);

            Result? result;
            if (resultadoIdentity.Succeeded)
            {
                var identityUser = await _signManager
                    .UserManager
                    .Users
                    .FirstOrDefaultAsync(user => user.NormalizedUserName == loginRequest.Username);
                var token = await _tokenService.CreateToken(identityUser);
                result = Result.Ok().WithSuccess(token.Value);
            }
            else
            {
                result = Result.Fail("Login error");
            }
            return result;
        }
    }


}
