using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SondagemNectaAPI.Models
{
    [Table("usuario")]
    public class UsuarioBO
    {
        [Key]
        public int Id { get; set; }

        [Column("user_login")]
        public string Login { get; set; }

        [Column("password")]
        public string Senha { get; set; }

        [Column("user_name")]
        public string NomeUsuario { get; set; }

        [Column("user_profile")]
        public string PerfilUsuario { get; set; }
    }
}
