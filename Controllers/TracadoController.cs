using Microsoft.AspNetCore.Mvc;
using SondagemNectaAPI.Interfaces;
using SondagemNectaAPI.Models;

namespace SondagemNectaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/tracado")]
    public class TracadoController : ControllerBase
    {
        private readonly ITracado _tracadoRepository;

        public TracadoController(ITracado tracadoRepository)
        {
            _tracadoRepository = tracadoRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tracado = _tracadoRepository.Get();
            return Ok(tracado);
        }
    }
}
