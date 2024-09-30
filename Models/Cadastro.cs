using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SondagemNectaAPI.Models
{
    [Table("dados_sondagem_old")]
    public class Cadastro
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome_sondagem")]
        public string CodigoPonto { get; set; }

        [Column("profundidade_programada")]
        public string ProfundidadeProgramada { get; set; }

        [Column("profundidade_final")]
        public string ProfundidadeFinal { get; set; }

        [Column("latitude_utm")]
        public string LatitudeUTM { get; set; }

        [Column("longitude_utm")]
        public string LongitudeUTM { get; set; }

        [Column("latitude_prevista_sondagem")]
        public string LatitudePrevista { get; set; }

        [Column("longitude_prevista_sondagem")]
        public string LongitudePrevista { get; set ; }

        [Column("latitude_sondagem")]
        public string LatitudeReal { get; set; }

        [Column("longitude_sondagem")]
        public string LongitudeReal { get; set; }

        [Column("rodovia_sondagem")]
        public string Rodovia {  get; set; }

        [Column("nome_sondadores")]
        public string NomeSondadores { get; set; }

        [Column("status_sondagem")]
        public string StatusSondagem { get; set; }

        [Column("observacao_sondagem")]
        public string Observacao { get; set; }

        [Column("data_hora_coleta_sondagem")]
        public string DataColeta { get; set; }

        [Column("inclusao_data_novo")]
        public string DataInclusao { get; set; }

        [Column("data_inicio_coleta")]
        public string DataInicio { get; set; }

        [Column("data_fim_coleta")]
        public string DataFim { get; set; }

        [Column("nome_ponto")]
        public string NomePonto { get; set; }

        [Column("nome_foto_execucao_sondagem")]
        public string NomeFotoExecucao { get; set; }

        [Column("nome_fotos_coleta")]
        public string NomeFotoColeta { get; set; }

        [Column("descricao_coleta")]
        public string DescricaoColeta { get; set; }

        [Column("nome_foto_boletim")]
        public string NomeFotoBoletim {  get; set; }

        [Column("nome_foto_furo_fechado")]
        public string NomeFotoFuroFechado { get; set; }


        [Column("caminho_fotos_execucao_sondagem")]
        public string CaminhoFotoExecucao { get; set; }

        [Column("caminho_fotos_coleta")]
        public string CaminhoFotoColeta { get; set; }

        [Column("caminho_foto_boletim")]
        public string CaminhoFotoBoletim { get; set; }

        [Column("caminho_foto_furo_fechado")]
        public string CaminhoFotoFuroFechado { get; set; }
    }
}
