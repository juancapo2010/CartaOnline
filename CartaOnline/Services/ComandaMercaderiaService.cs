using CartaOnline.DTO;
using CartaOnline.Models;
using CartaOnline.Repositories;

namespace CartaOnline.Services
{
    public interface IComandaMercaderiaService
    {
        ComandaMercaderia AgregarMercaderia(ComandaMercaderiaDto comandamercaderia);
        ComandaMercaderia GetComandaMercaderias(int id);
    }
    public class ComandaMercaderiaService : IComandaMercaderiaService
    {
        private readonly IRepositoryGeneric _repository;
        public ComandaMercaderiaService(IRepositoryGeneric repository)
        {
            _repository = repository;
        }
        public ComandaMercaderia AgregarMercaderia(ComandaMercaderiaDto comandamercaderia)
        {
            var entity = new ComandaMercaderia
            {
                MercaderiaId = comandamercaderia.MercaderiaId,
                ComandaId = comandamercaderia.ComandaId
            };
            _repository.Add<ComandaMercaderia>(entity);
            return entity;
        }

        public ComandaMercaderia GetComandaMercaderias(int id)
        {
            return _repository.FindBy<ComandaMercaderia>(id);
        }
    }
}
