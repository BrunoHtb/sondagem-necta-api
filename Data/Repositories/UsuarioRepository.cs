using SondagemNectaAPI.Interfaces;
using SondagemNectaAPI.Models;

namespace SondagemNectaAPI.Data.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        private readonly ConnectionContext _connectionContext;

        public UsuarioRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        public List<UsuarioApp> GetUserApp()
        {
            return _connectionContext.UsuariosApp.OrderBy(x => x.Id).ToList();
        }

        public List<UsuarioBackOffice> GetUserBackOffice()
        {
            return _connectionContext.UsuariosBackOffice.OrderBy(x => x.Id).ToList();
        }
    }
}
