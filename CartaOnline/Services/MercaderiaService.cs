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
        ResponseGetMercaderiaById GetMercaderiaId(int id);
        List<ResponseGetMercaderiaTipoDto> GetMercaderiaByTipo(string tipo);
        void DeleteMercaderiaId(int id);
        MercaderiaUpdateDto UpdateMercaderia(int id, MercaderiaUpdateDto mercaderia);
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
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else {
                _repository.DeleteBy<Mercaderia>(id);
            }
            
        }

        public List<ResponseGetMercaderiaTipoDto> GetMercaderiaByTipo(string tipo)
        {
            return _query.GetMercaderiaByTipo(tipo);
        }

        public ResponseGetMercaderiaById GetMercaderiaId(int id)
        {
            
            var mercaderia = _repository.FindBy<Mercaderia>(id);
            if (mercaderia == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return new ResponseGetMercaderiaById
            {
                id = mercaderia.MercaderiaId,
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

        public MercaderiaUpdateDto UpdateMercaderia(int id, MercaderiaUpdateDto mercaderia)
        {
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
            return new MercaderiaUpdateDto
            {
                Nombre = entity.Nombre,
                Tipo = entity.TipoMercaderiaId,
                Precio = entity.Precio,
                Ingredientes = entity.Ingredientes,
                Preparacion = entity.Preparacion,
                Imagen = entity.Imagen
            };
        }

        public bool validarMercaderias(int id)
        {
            bool existe = false;

                if (_repository.FindBy<Mercaderia>(id) != null)
                {
                    existe = true;
                }
            
            return existe;
        }
    }
}
