using Microsoft.AspNetCore.Mvc;

namespace UsuariosApi.Controllers
{
    public interface IUsuarioController
    {
        public Task<ActionResult> CreateUsuario(CreateUsuarioDto createUsuarioDto); 
    }
}