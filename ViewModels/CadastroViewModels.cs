using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SondagemNectaAPI.ViewModels
{
    public class CadastroViewModels
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nome_ponto")]
        public string Nome { get; set; }
        [Column("status_sondagem")]
        public string Status { get; set; }
        [Column("latitude_utm")]
        public string LatitudeUTM { get; set; }
        [Column("longitude_utm")]
        public string LongitudeUTM { get; set; }
        [Column("rodovia_sondagem")]
        public string Rodovia {  get; set; }
        [Column("profundidade_programada")]
        public string ProfundidadeProgramada { get; set; }
        [Column("profundidade_final")]
        public string ProfundidadeFinal {  get; set; }
        [Column("observacao_sondagem")]
        public string Observacao { get; set; }
        [Column("nome_sondadores")]
        public string Equipe { get; set; }
       
    }
}
