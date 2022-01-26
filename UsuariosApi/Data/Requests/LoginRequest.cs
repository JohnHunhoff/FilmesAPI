using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Controllers
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}