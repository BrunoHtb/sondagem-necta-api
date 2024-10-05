using Microsoft.AspNetCore.Mvc;
using SondagemNectaAPI.Data.Repositories;
using SondagemNectaAPI.Interfaces;
using SondagemNectaAPI.ViewModels;
using System.IO.Compression;
using System.Reflection.Metadata;

namespace SondagemNectaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/relatorio")]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorio _relatorioRepository;

        public RelatorioController(IRelatorio relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
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
        /*
        [HttpPost("gerar-relatorio")]
        public async Task<IActionResult> GerarRelatorio([FromBody] List<int> idsList)
        {
            if (idsList == null || idsList.Count == 0)
            {
                return BadRequest("A lista de IDs está vazia.");
            }
            
            //var documentos = await GerarDocumentosWord(ids);

            using (var ms = new MemoryStream())
            {
                using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (var doc in documentos)
                    {
                        var entry = zip.CreateEntry(doc.FileName);

                        using (var entryStream = entry.Open())
                        {
                            await entryStream.WriteAsync(doc.FileContent, 0, doc.FileContent.Length);
                        }
                    }
                }

                // Resetar a posição do MemoryStream
                ms.Position = 0;

                // Retornar o arquivo ZIP
                return File(ms.ToArray(), "application/zip", "Relatorios.zip");
            }
            */
    }
}

