using Microsoft.AspNetCore.Mvc;
using SondagemNectaAPI.Models;

namespace SondagemNectaAPI.Interfaces
{
    public interface ICadastro
    {
        void Add(Cadastro cadastro);

        List<Cadastro> Get();

        Cadastro GetById(int id); 

        void Update(Cadastro cadastro); 

        void Delete(int id);
    }
}
