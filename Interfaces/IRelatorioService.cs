using SondagemNectaAPI.Models;

namespace SondagemNectaAPI.Interfaces
{
    public interface IRelatorioService
    {
        Task<List<RelatorioGerado>> GerarRelatorios(List<int> ids);
    }
}
