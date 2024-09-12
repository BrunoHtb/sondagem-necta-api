using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SondagemNectaAPI.Interfaces;
using SondagemNectaAPI.Models;
using SondagemNectaAPI.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SondagemNectaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuarioRepository;
        public UsuarioController(IUsuario usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet("app")]
        public IActionResult GetApp()
        {
            var usuario = _usuarioRepository.GetUserApp();
            return Ok(usuario);
        }

        [HttpGet("backoffice")]
        public IActionResult GetBackOffice()
        {
            var usuario = _usuarioRepository.GetUserBackOffice();
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var usuario = _usuarioRepository.GetUserBackOffice().FirstOrDefault(x => x.Login == loginModel.Usuario
                                                                                 && x.Senha == loginModel.Senha);
            
            if(usuario == null)
            {
                return Unauthorized("Usuário ou senha inválidos");
            }

            return Ok(new
            {
                usuario = usuario.Login
            });
        }

    }
}
