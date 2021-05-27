using CartaOnline.DTO;
using CartaOnline.Models;
using CartaOnline.Query;
using CartaOnline.Repositories;
using System.Collections.Generic;


namespace CartaOnline.Services
{
    public interface IMercaderiaService
    {
        Mercaderia CreateMercaderia(MercaderiaDto mercaderia);
        IEnumerable<Mercaderia> GetMercaderia();
        Mercaderia GetMercaderiaId(int id);
        List<ResponseGetMercaderiaTipoDto> GetMercaderiaByTipo(string tipo);
        void DeleteMercaderiaId(int id);
        Mercaderia UpdateMercaderia(MercaderiaUpdateDto mercaderia);
    }
    public class Mercaderiaervice : IMercaderiaService
    {
        private readonly IRepositoryGeneric _repository;
        private readonly IMercaderiaQuery _query;
        public Mercaderiaervice(IRepositoryGeneric repository, IMercaderiaQuery query)
        {
            _repository = repository;
            _query = query;
        }

        public Mercaderia CreateMercaderia(MercaderiaDto mercaderia)
        {
            var entity = new Mercaderia
            {
                Nombre = mercaderia.Nombre,
                Precio = mercaderia.Precio,
                Ingredientes = mercaderia.Ingredientes,
                Preparacion = mercaderia.Preparacion,
                Imagen = mercaderia.Imagen,
                TipoMercaderiaId = mercaderia.TipoMercaderiaId
            };
            _repository.Add<Mercaderia>(entity);
            return entity;
        }

        public void DeleteMercaderiaId(int id)
        {
            _repository.DeleteBy<Mercaderia>(id);
        }

        public List<ResponseGetMercaderiaTipoDto> GetMercaderiaByTipo(string tipo)
        {
            return _query.GetComandaByTipo(tipo);
        }

        public Mercaderia GetMercaderiaId(int id)
        {
            return _repository.FindBy<Mercaderia>(id);
        }

        public IEnumerable<Mercaderia> GetMercaderia()
        {
            return _repository.Traer<Mercaderia>();
        }

        public Mercaderia UpdateMercaderia(MercaderiaUpdateDto mercaderia)
        {
            var entity = new Mercaderia
            {
                MercaderiaId = mercaderia.MercaderiaId,
                Nombre = mercaderia.Nombre,
                Precio = mercaderia.Precio,
                Ingredientes = mercaderia.Ingredientes,
                Preparacion = mercaderia.Preparacion,
                Imagen = mercaderia.Imagen,
                TipoMercaderiaId = mercaderia.TipoMercaderiaId
            };
            _repository.Update(entity);
            return entity;
        }
    }
}
