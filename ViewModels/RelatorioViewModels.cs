using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SondagemNectaAPI.ViewModels
{
    public class RelatorioViewModels
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }
        public string? Rodovia { get; set; }
        public string? ProfundidadeProgramada { get; set; }
        public string? ProfundidadeFinal { get; set; }
        public string? DataInicioColeta { get; set; }
        public string? DataFimColeta { get; set; }

    }
}
