using SondagemNectaAPI.Interfaces;
using SondagemNectaAPI.Models;

namespace SondagemNectaAPI.Data.Repositories
{
    public class RelatorioRepository : IRelatorio
    {
        private readonly ConnectionContext _connectionContext;

        public RelatorioRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        public List<Cadastro> Get()
        {
            return _connectionContext.Cadastros.OrderBy(x => x.NomePonto).ToList();
        }
    }
}
