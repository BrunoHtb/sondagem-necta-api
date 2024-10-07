using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SondagemNectaAPI.Models
{
    [Table("tracado")]
    public class Tracado
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("descricao")]
        public string Descricao { get; set; }
        [Column("coordenadas")]
        public string Coordenadas { get; set; }
    }
}
