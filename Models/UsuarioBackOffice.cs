using System.ComponentModel.DataAnnotations.Schema;

namespace SondagemNectaAPI.Models
{
    [Table("usuario")]
    public class UsuarioBackOffice : Usuario
    {
        [Column("senha")]
        public string Senha { get; set; }
    }
}
