using Microsoft.AspNetCore.Mvc;
using SondagemNectaAPI.Models;
using SondagemNectaAPI.ViewModels;

namespace SondagemNectaAPI.Interfaces
{
    public interface ICadastro
    {
        void Add(Cadastro cadastro);

        List<Cadastro> Get();

        Cadastro GetById(int id);

        List<Cadastro> GetById(List<int> idsList);

        void Update(Cadastro cadastro); 

        void Delete(int id);
    }
}
