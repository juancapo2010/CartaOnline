using CartaOnline.DTO;
using CartaOnline.Models;
using CartaOnline.Query;
using CartaOnline.Repositories;
using System.Collections.Generic;

namespace CartaOnline.Services
{
    public interface IComandaService
    {
        ComandaDto CreateComanda(ComandaDto comanda);
        IEnumerable<ResponseGetAllComandaDto> GetComanda(string hora);
        ResponseGetComandaById GetComandaId(int id);

    }
    public class Comandaervice : IComandaService
    {
        private readonly IRepositoryComanda _repository;
        private readonly IComandaQuery _query;
        public Comandaervice(IRepositoryComanda repository, IComandaQuery query)
        {
            _repository = repository;
            _query = query;
        }

        public ComandaDto CreateComanda(ComandaDto comanda)
        {
            bool validacionFromaEntrega = validarFormaEntrega(comanda.FormaEntrega);
            bool validacionMercaderias = validarMercaderias(comanda.Mercaderias);
            return _repository.CreateComanda(comanda, validacionFromaEntrega, validacionMercaderias);           
        }

        public ResponseGetComandaById GetComandaId(int id)
        {
            return _query.GetComandaById(id);
        }

        public IEnumerable<ResponseGetAllComandaDto> GetComanda(string hora)
        {
            return _query.GetAllComanda(hora);
        }

        public bool validarFormaEntrega(int id)
        {
            bool existe = false;

            if (_repository.FindBy<FormaEntrega>(id) != null)
            {
                existe = true;
            }


            return existe;
        }
        public bool validarMercaderias(List<int> comanda)
        {
            bool existe = false;

            foreach (var validar in comanda)
            {
                if (_repository.FindBy<Mercaderia>(validar) != null)
                {
                    existe = true;
                }
            }
            return existe;
        }
    }
}
