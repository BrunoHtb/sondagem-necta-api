using Microsoft.AspNetCore.Mvc;
using SondagemNectaAPI.Interfaces;
using SondagemNectaAPI.ViewModels;
using System.IO.Compression;

namespace SondagemNectaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/relatorio")]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorio _relatorioRepository;
        private readonly IRelatorioService _relatorioService;

        public RelatorioController(IRelatorio relatorioRepository, IRelatorioService relatorioService)
        {
            _relatorioRepository = relatorioRepository;
            _relatorioService = relatorioService;
        }

        [HttpGet("listar-relatorio")]
        public IActionResult GetFilter()
        {
            var cadastros = _relatorioRepository.Get();
            var relatorioViewModels = cadastros.Select(cadastro => new RelatorioViewModels
            {
                Id = cadastro.Id,
                Nome = cadastro.NomePonto,
                Status = cadastro.StatusSondagem,
                Rodovia = cadastro.Rodovia,
                ProfundidadeProgramada = cadastro.ProfundidadeProgramada,
                ProfundidadeFinal = cadastro.ProfundidadeFinal,
                DataInicioColeta = cadastro.DataInicio.Split("_")[0],
                DataFimColeta = cadastro.DataFim.Split("_")[0]
            }).ToList();

            return Ok(relatorioViewModels);
        }

        [HttpPost("gerar-relatorio")]
        public async Task<IActionResult> GerarRelatorio([FromBody] List<int> idsList)
        {
            if (idsList == null || idsList.Count == 0)
            {
                return BadRequest("A lista de IDs está vazia.");
            }

            var documentos = await _relatorioService.GerarRelatorios(idsList);
            var arquivosGerados = documentos.Select(d => d.NomeArquivo).ToList();

            try
            {
                using (var ms = new MemoryStream())
                {
                    using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                    {
                        foreach (var doc in documentos)
                        {
                            var entry = zip.CreateEntry(doc.NomeArquivo);

                            using (var entryStream = entry.Open())
                            {
                                await entryStream.WriteAsync(doc.ConteudoArquivo, 0, doc.ConteudoArquivo.Length);
                            }
                        }
                    }
                    ms.Position = 0;

                    return File(ms.ToArray(), "application/zip", "Relatorios.zip");
                }
            }
            finally
            {
                foreach (var arquivo in arquivosGerados)
                {
                    if (System.IO.File.Exists(arquivo))
                    {
                        try
                        {
                            System.IO.File.Delete(arquivo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao deletar o arquivo {arquivo}: {ex.Message}");
                        }
                    }
                }
            }
        }

    }
}

