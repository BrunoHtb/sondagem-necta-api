using SondagemNectaAPI.Models;

namespace SondagemNectaAPI.Interfaces
{
    public interface IUsuario
    {
        List<UsuarioApp> GetUserApp();
        List<UsuarioBackOffice> GetUserBackOffice();
    }
}
