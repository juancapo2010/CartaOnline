using CartaOnline.DTO;
using CartaOnline.Models;
using CartaOnline.Repositories;

namespace CartaOnline.Services
{
    public interface IComandaMercaderiaervice
    {
        ComandaMercaderia AgregarMercaderia(ComandaMercaderiaDto comandamercaderia);
        ComandaMercaderia GetComandaMercaderia(int id);
    }
    public class ComandaMercaderiaervice : IComandaMercaderiaervice
    {
        private readonly IRepositoryGeneric _repository;
        public ComandaMercaderiaervice(IRepositoryGeneric repository)
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

        public ComandaMercaderia GetComandaMercaderia(int id)
        {
            return _repository.FindBy<ComandaMercaderia>(id);
        }
    }
}
