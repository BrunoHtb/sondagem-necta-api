using SondagemNectaAPI.Interfaces;
using SondagemNectaAPI.Models;

namespace SondagemNectaAPI.Data.Repositories
{
    public class TracadoRepository : ITracado
    {
        private readonly ConnectionContext _connectionContext; 

        public TracadoRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        public List<Tracado> Get()
        {
            return _connectionContext.Tracados.ToList();
        }
    }
}
