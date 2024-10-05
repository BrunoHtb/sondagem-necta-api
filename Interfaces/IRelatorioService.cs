namespace SondagemNectaAPI.Interfaces
{
    public interface IRelatorioService
    {
        Task<string> GerarRelatoriosAsync(List<int> ids);

    }
}
