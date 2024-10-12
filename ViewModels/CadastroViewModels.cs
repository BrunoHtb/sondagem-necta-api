namespace SondagemNectaAPI.ViewModels
{
    public class CadastroViewModels
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }
        public string? LatitudeUTM { get; set; }
        public string? LongitudeUTM { get; set; }
        public string? Rodovia { get; set; }
        public string? ProfundidadeProgramada { get; set; }
        public string? ProfundidadeFinal { get; set; }
        public string? Observacao { get; set; }
        public string? Equipe { get; set; }
        public string? DescricaoColeta { get; set; }
        public string? CaminhoFotoExecucao { get; set; }
        public string? CaminhoFotoColeta { get; set; }
        public string? CaminhoFotoBoletim { get; set; }
        public string? CaminhoFotoFuroFechado { get; set; }
    }
}
