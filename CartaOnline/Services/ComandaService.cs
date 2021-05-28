using CartaOnline.DTO;
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
            return _repository.CreateComanda(comanda);           
        }

        public ResponseGetComandaById GetComandaId(int id)
        {
            return _query.GetComandaById(id);
        }

        public IEnumerable<ResponseGetAllComandaDto> GetComanda(string hora)
        {
            return _query.GetAllComanda(hora);
        }
    }
}
