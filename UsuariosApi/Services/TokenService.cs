using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using UsuariosApi.Models;

namespace UsuariosApi.Services;

public class TokenService
{
    public async Task<Token> CreateToken(IdentityUser<int> identityUser)
    {
        Claim[] direitosUsuarios = new Claim[]
        {
            new("username", identityUser.UserName),
            new("id", identityUser.Id.ToString())
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajs09asjd09sajcnzxn")
        );
        var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: direitosUsuarios,
            signingCredentials: credenciais,
            expires: DateTime.UtcNow.AddHours(1)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new Token(tokenString);
    }
}