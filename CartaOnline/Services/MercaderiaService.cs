using CartaOnline.DTO;
using CartaOnline.Models;
using CartaOnline.Query;
using CartaOnline.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace CartaOnline.Services
{
    public interface IMercaderiaService
    {
        MercaderiaDto CreateMercaderia(MercaderiaDto mercaderia);
        IEnumerable<Mercaderia> GetMercaderia();
        MercaderiaDto GetMercaderiaId(int id);
        List<ResponseGetMercaderiaTipoDto> GetMercaderiaByTipo(string tipo);
        void DeleteMercaderiaId(int id);
        Mercaderia UpdateMercaderia(int id, MercaderiaUpdateDto mercaderia);
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

        public MercaderiaDto CreateMercaderia(MercaderiaDto mercaderia)
        {
            var entity = new Mercaderia
            {
                Nombre = mercaderia.Nombre,
                Precio = mercaderia.Precio,
                Ingredientes = mercaderia.Ingredientes,
                Preparacion = mercaderia.Preparacion,
                Imagen = mercaderia.Imagen,
                TipoMercaderiaId = mercaderia.Tipo
            };
            _repository.Add<Mercaderia>(entity);
            return new MercaderiaDto
            {
                Nombre = mercaderia.Nombre,
                Tipo = mercaderia.Tipo,
                Precio = mercaderia.Precio,
                Ingredientes = mercaderia.Ingredientes,
                Preparacion = mercaderia.Preparacion,
                Imagen = mercaderia.Imagen
            };
        }

        public void DeleteMercaderiaId(int id)
        {
            var mercaderia = _repository.FindBy<Mercaderia>(id);
            if (mercaderia == null)
            {
                //de alguna forma tendria que pasar un mensaje al body
            }
            else {
                _repository.DeleteBy<Mercaderia>(id);
            }
            
        }

        public List<ResponseGetMercaderiaTipoDto> GetMercaderiaByTipo(string tipo)
        {
            return _query.GetComandaByTipo(tipo);
        }

        public MercaderiaDto GetMercaderiaId(int id)
        {
            
            var mercaderia = _repository.FindBy<Mercaderia>(id);
            if (mercaderia == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return new MercaderiaDto
            {
                Nombre = mercaderia.Nombre,
                Tipo=mercaderia.TipoMercaderiaId,
                Precio=mercaderia.Precio,
                Ingredientes=mercaderia.Ingredientes,
                Preparacion=mercaderia.Preparacion,
                Imagen=mercaderia.Imagen
            };
        }

        public IEnumerable<Mercaderia> GetMercaderia()
        {
            return _repository.Traer<Mercaderia>();
        }

        public Mercaderia UpdateMercaderia(int id, MercaderiaUpdateDto mercaderia)
        {
            var validacion = _repository.FindBy<Mercaderia>(id);
            if (validacion == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var entity = new Mercaderia
            {
                MercaderiaId = id,
                Nombre = mercaderia.Nombre,
                Precio = mercaderia.Precio,
                Ingredientes = mercaderia.Ingredientes,
                Preparacion = mercaderia.Preparacion,
                Imagen = mercaderia.Imagen,
                TipoMercaderiaId = mercaderia.Tipo
            };
            _repository.Update(entity);
            return entity;
        }
    }
}
