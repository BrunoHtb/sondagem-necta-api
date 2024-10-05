using SondagemNectaAPI.Data.Repositories;
using SondagemNectaAPI.Interfaces;

namespace SondagemNectaAPI.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly RelatorioRepository _relatorioRepository;

        public Task<string> GerarRelatoriosAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
