using Microsoft.EntityFrameworkCore;
using SondagemNectaAPI.Models;

namespace SondagemNectaAPI.Data
{
    public class ConnectionContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ConnectionContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Cadastro> Cadastros { get; set; }
        public DbSet<UsuarioApp> UsuariosApp { get; set; }
        public DbSet<UsuarioBackOffice> UsuariosBackOffice { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }
}
