using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using SondagemNectaAPI.Interfaces;
using SondagemNectaAPI.Models;

namespace SondagemNectaAPI.Data.Repositories
{
    public class CadastroRepository : ICadastro
    {
        private readonly ConnectionContext _connectionContext;

        public CadastroRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        public void Add(Cadastro cadastro)
        {
            _connectionContext.Cadastros.Add(cadastro);
            _connectionContext.SaveChanges();
        }

        public List<Cadastro> Get()
        {
            return _connectionContext.Cadastros.OrderBy(x => x.NomePonto).ToList();
        }

        public Cadastro GetById(int id)
        {
            return _connectionContext.Cadastros.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Cadastro cadastro)
        {
            var cadastroExistente = _connectionContext.Cadastros.FirstOrDefault(x => x.Id == cadastro.Id);
            if(cadastroExistente != null)
            {
                cadastroExistente.CodigoPonto = cadastro.CodigoPonto;
                cadastroExistente.ProfundidadeProgramada = cadastro.ProfundidadeProgramada;
                cadastroExistente.ProfundidadeFinal = cadastro.ProfundidadeFinal;
                cadastroExistente.LatitudeUTM = cadastro.LatitudeUTM;
                cadastroExistente.LongitudeUTM = cadastro.LongitudeUTM;
                cadastroExistente.Rodovia = cadastro.Rodovia;
                cadastroExistente.NomeSondadores = cadastro.NomeSondadores;
                cadastroExistente.StatusSondagem = cadastro.StatusSondagem;
                cadastroExistente.Observacao = cadastro.Observacao;
                cadastroExistente.NomePonto = cadastro.NomePonto;
                cadastroExistente.DescricaoColeta = cadastro.DescricaoColeta;

                _connectionContext.Cadastros.Update(cadastroExistente);
                _connectionContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var cadastro = _connectionContext.Cadastros.FirstOrDefault(c => c.Id == id);
            if(cadastro != null)
            {
                _connectionContext.Cadastros.Remove(cadastro);
                _connectionContext.SaveChanges();
            }
        }

    }
}
