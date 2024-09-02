using Microsoft.AspNetCore.Mvc;
using SondagemNectaAPI.Data.Repositories;
using SondagemNectaAPI.Interfaces;
using SondagemNectaAPI.Models;

namespace SondagemNectaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/cadastro")]
    public class CadastroController : ControllerBase
    {
        private readonly ICadastro _cadastroRepository;

        public CadastroController(ICadastro cadastroRepository)
        {
            _cadastroRepository = cadastroRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var cadastro = _cadastroRepository.Get();
            return Ok(cadastro);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Cadastro cadastro)
        {
            if(id != cadastro.Id)
            {
                return BadRequest("ID in the URL does not match the ID in the body");
            }

            var cadastroExistente = _cadastroRepository.GetById(id);
            if(cadastroExistente == null)
            {
                return NotFound("Cadastro Not Found!");
            }

            _cadastroRepository.Update(cadastro);
            return NoContent();
        }

    }
}
