using Microsoft.AspNetCore.Mvc;
using SondagemNectaAPI.Interfaces;

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
    }
}
