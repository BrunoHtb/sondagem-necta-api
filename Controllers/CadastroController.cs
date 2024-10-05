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
                Equipe = cadastro.NomeSondadores,
                DescricaoColeta = cadastro.DescricaoColeta,

                CaminhoFotoBoletim = cadastro.CaminhoFotoBoletim,
                CaminhoFotoColeta = cadastro.CaminhoFotoColeta,
                CaminhoFotoExecucao = cadastro.CaminhoFotoExecucao,
                CaminhoFotoFuroFechado = cadastro.CaminhoFotoFuroFechado
            };
            return Ok(cadastroViewModel);
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CadastroViewModels cadastroViewModel)
        {
            var cadastroModel = _cadastroRepository.GetById(id);
            if (cadastroModel == null) return NotFound();

            cadastroModel.NomePonto = string.IsNullOrEmpty(cadastroViewModel.Nome) ? "" : cadastroViewModel.Nome;
            cadastroModel.StatusSondagem = string.IsNullOrEmpty(cadastroViewModel.Status) ? "" : cadastroViewModel.Status;
            cadastroModel.LatitudeUTM = string.IsNullOrEmpty(cadastroViewModel.LatitudeUTM) ? "" : cadastroViewModel.LatitudeUTM;
            cadastroModel.LongitudeUTM = string.IsNullOrEmpty(cadastroViewModel.LongitudeUTM) ? "" : cadastroViewModel.LongitudeUTM;
            cadastroModel.Rodovia = string.IsNullOrEmpty(cadastroViewModel.Rodovia) ? "" : cadastroViewModel.Rodovia;
            cadastroModel.ProfundidadeProgramada = string.IsNullOrEmpty(cadastroViewModel.ProfundidadeProgramada) ? "" : cadastroViewModel.ProfundidadeProgramada;
            cadastroModel.ProfundidadeFinal = string.IsNullOrEmpty(cadastroViewModel.ProfundidadeFinal) ? "" : cadastroViewModel.ProfundidadeFinal;
            cadastroModel.Observacao = string.IsNullOrEmpty(cadastroViewModel.Observacao) ? "" : cadastroViewModel.Observacao;
            cadastroModel.NomeSondadores = string.IsNullOrEmpty(cadastroViewModel.Equipe) ? "" : cadastroViewModel.Equipe;
            cadastroModel.CaminhoFotoExecucao = string.IsNullOrEmpty(cadastroModel.CaminhoFotoExecucao) ? "" : cadastroViewModel.CaminhoFotoExecucao;
            cadastroModel.CaminhoFotoColeta = string.IsNullOrEmpty(cadastroViewModel.CaminhoFotoColeta) ? "" : cadastroViewModel.CaminhoFotoColeta;

            foreach (var formFile in Request.Form.Files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = formFile.FileName;
                    var subDirectory = $"{cadastroModel.CodigoPonto}_{cadastroModel.Rodovia}";
                    var pathSubdirectory = Path.Combine("D:\\xampp\\htdocs\\Repositorio\\Dados_SondagemSP_2024", subDirectory);
                    var path = Path.Combine(pathSubdirectory, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            _cadastroRepository.Update(cadastroModel);
            return NoContent();
        }

    }
}
