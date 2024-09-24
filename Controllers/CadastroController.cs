using Microsoft.AspNetCore.Mvc;
using SondagemNectaAPI.Data.Repositories;
using SondagemNectaAPI.Interfaces;
using SondagemNectaAPI.Models;
using SondagemNectaAPI.ViewModels;

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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var cadastro = _cadastroRepository.GetById(id);

            if (cadastro == null) return NotFound();

            var cadastroViewModel = new CadastroViewModels
            {
                Id = cadastro.Id,
                Nome = cadastro.NomePonto,
                Status = cadastro.StatusSondagem,
                LatitudeUTM = cadastro.LatitudeUTM,
                LongitudeUTM = cadastro.LongitudeUTM,
                Rodovia = cadastro.Rodovia,
                ProfundidadeProgramada = cadastro.ProfundidadeProgramada,
                ProfundidadeFinal = cadastro.ProfundidadeFinal,
                Observacao = cadastro.Observacao,
                Equipe = cadastro.NomeSondadores
            };

            return Ok(cadastroViewModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CadastroViewModels cadastroViewModel)
        {
            var cadastroModel = _cadastroRepository.GetById(id);
            if (cadastroModel == null) return NotFound();

            cadastroModel.NomePonto = cadastroViewModel.Nome;
            cadastroModel.StatusSondagem = cadastroViewModel.Status;
            cadastroModel.LatitudeUTM = cadastroViewModel.LatitudeUTM;
            cadastroModel.LongitudeUTM = cadastroViewModel.LongitudeUTM;
            cadastroModel.Rodovia = cadastroViewModel.Rodovia;
            cadastroModel.ProfundidadeProgramada = cadastroViewModel.ProfundidadeProgramada;
            cadastroModel.ProfundidadeFinal = cadastroViewModel.ProfundidadeFinal;
            cadastroModel.Observacao = cadastroViewModel.Observacao;
            cadastroModel.NomeSondadores = cadastroViewModel.Equipe;

            _cadastroRepository.Update(cadastroModel);
            return NoContent();
        }

    }
}
