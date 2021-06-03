using CartaOnline.Config;
using CartaOnline.DTO;
using CartaOnline.Models;
using Microsoft.EntityFrameworkCore;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace CartaOnline.Repositories
{
    public interface IRepositoryComanda
    {
        void Add<T>(T entity) where T : class;
        List<T> Traer<T>() where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteBy<T>(int id) where T : class;
        T FindBy<T>(int id) where T : class;
        ComandaDto CreateComanda(ComandaDto comanda, bool validacionFromaEntrega, bool validacionMercaderias);


    }
    public class RepositoryComanda : IRepositoryComanda
    {
        private readonly AppDbContext _context;
        private readonly IDbConnection _connection;
        private readonly Compiler _SqlKataCompiler;

        public RepositoryComanda(AppDbContext context, IDbConnection connection, Compiler SqlKataCompiler)
        {
            _context = context;
            _connection = connection;
            _SqlKataCompiler = SqlKataCompiler;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteBy<T>(int id) where T : class
        {
            T entity = FindBy<T>(id);
            Delete<T>(entity);
        }

        public T FindBy<T>(int id) where T : class
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> Traer<T>() where T : class
        {
            List<T> query = _context.Set<T>().ToList();
            return query;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public ComandaDto CreateComanda(ComandaDto comanda, bool validacionFromaEntrega, bool validacionMercaderias)
        {
            var validacionformaentrega = validacionFromaEntrega;
            var validacionmercaderias = validacionMercaderias;
            if (validacionformaentrega && validacionmercaderias)
            {
                var db = new QueryFactory(_connection, _SqlKataCompiler);
                var id = db.Query("Comanda").InsertGetId<int>(
                    new
                    {
                        FormaEntregaId = comanda.FormaEntrega,
                        Fecha = DateTime.Now,
                        PrecioTotal = 0
                    }
                    );
                try
                {
                    foreach (var mercaderia in comanda.Mercaderias)
                    {
                        db.Query("ComandaMercaderias").Insert(
                        new
                        {
                            ComandaId = id,
                            MercaderiaId = mercaderia,
                        }
                        );

                    }
                    int suma = 0;
                    foreach (var actualizapreciototal in comanda.Mercaderias)
                    {
                        var actualizo = db.Query("Mercaderias")
                            .Select()
                            .Where("MercaderiaId", "=", actualizapreciototal)
                            .FirstOrDefault<ResponseGetComandaByIdMercaderia>();
                        suma = actualizo.Precio + suma;

                    }
                    db.Query("Comanda").Where("ComandaId", "=", id).Update(new { precioTotal = suma });
                    return comanda;
                }
                catch
                {
                    DeleteBy<Comanda>(id);
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
 
    }
}
