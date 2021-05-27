using CartaOnline.DTO;
using CartaOnline.Query;
using CartaOnline.Repositories;
using System.Collections.Generic;

namespace CartaOnline.Services
{
    public interface IComandaService
    {
        ComandaDto CreateComanda(ComandaDto comanda);
        IEnumerable<ResponseGetAllComandaDto> GetComandas(string hora);
        ResponseGetComandaById GetComandaId(int id);

    }
    public class ComandaService : IComandaService
    {
        private readonly IRepositoryGeneric _repository;
        private readonly IComandaQuery _query;
        public ComandaService(IRepositoryGeneric repository, IComandaQuery query)
        {
            _repository = repository;
            _query = query;
        }

        public ComandaDto CreateComanda(ComandaDto comanda)
        {
            return _query.CreateComanda(comanda);           
        }

        public ResponseGetComandaById GetComandaId(int id)
        {
            return _query.GetComandaById(id);
        }

        public IEnumerable<ResponseGetAllComandaDto> GetComandas(string hora)
        {
            return _query.GetAllComandas(hora);
        }
    }
}
