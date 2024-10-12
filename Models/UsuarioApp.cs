using System.ComponentModel.DataAnnotations.Schema;

namespace SondagemNectaAPI.Models
{
    [Table("usuario_app")]
    public class UsuarioApp : Usuario
    {
        [Column("cargo")]
        public string Cargo { get; set; }
    }
}
