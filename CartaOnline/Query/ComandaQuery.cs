using CartaOnline.DTO;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CartaOnline.Query
{
    public interface IComandaQuery
    {
        List<ResponseGetAllComandaDto> GetAllComandas(string hora);
        ResponseGetComandaById GetComandaById(int id);
        ComandaDto CreateComanda(ComandaDto comanda);

    }
    public class ComandaQuery : IComandaQuery
    {
        private readonly IDbConnection _connection;
        private readonly Compiler _SqlKataCompiler;

        public ComandaQuery(IDbConnection connection, Compiler SqlKataCompiler)
        {
            _connection = connection;
            _SqlKataCompiler = SqlKataCompiler;
        }

        public ComandaDto CreateComanda(ComandaDto comanda)
        {
            var db = new QueryFactory(_connection, _SqlKataCompiler);
            var id = db.Query("Comandas").InsertGetId<int>(
                new
                {
                    comanda.FormaEntregaId,
                    Fecha = DateTime.Now,
                    PrecioTotal = 0
                }
                );
            foreach(var mercaderias in comanda.Mercaderias.ToList())
            {
                db.Query("ComandaMercaderias").Insert(
                new
                {
                    ComandaId = id,
                    mercaderias.MercaderiaId
                }
                );
                
            }
            int suma = 0;
            foreach (var actualizapreciototal in comanda.Mercaderias.ToList())
            {
                var actualizo = db.Query("Mercaderias")
                    .Select()
                    .Where("MercaderiaId", "=", actualizapreciototal.MercaderiaId)
                    .FirstOrDefault<ResponseGetComandaByIdMercaderias>();
                 suma = actualizo.Precio + suma;

            }
            db.Query("Comandas").Where("ComandaId", "=", id).Update(new { precioTotal = suma });
            return comanda;
        }

        public List<ResponseGetAllComandaDto> GetAllComandas(string hora)
        {
            var db = new QueryFactory(_connection, _SqlKataCompiler);

            var comanda = db.Query("Comandas")
                .Select("Comandas.ComandaId",
                "Comandas.PrecioTotal",
                "Comandas.Fecha",
                "Comandas.FormaEntregaId")
                .OrderByDesc("Fecha")
                .When(!string.IsNullOrWhiteSpace(hora), q => q.WhereLike("Comandas.Fecha", $"%{hora}%"));
            var ComandaResult = comanda.Get<ResponseGetAllComandaDto>();

            List<ResponseGetAllComandaDto> result = new List<ResponseGetAllComandaDto>();
            foreach (var c in ComandaResult)
            {
                var formaentrega = db.Query("FormasEntregas")
                    .Select("FormaEntregaId", "Descripcion")
                    .Where("FormaEntregaId", "=", c.FormaEntregaId)
                    .FirstOrDefault<ResponseGetFormaEntregaByComanda>();

                var mercaderias = db.Query("ComandaMercaderias")
                    .Select()
                    .Where("ComandaId", "=",c.ComandaId )
                    .Join("Mercaderias", "Mercaderias.MercaderiaId", "ComandaMercaderias.MercaderiaId");

                var MercaderiaResult = mercaderias.Get<ResponseGetMercaderiasByComanda>();

                result.Add(
                     new ResponseGetAllComandaDto
                    {
                        ComandaId = c.ComandaId,
                        PrecioTotal = c.PrecioTotal,
                        Fecha = c.Fecha,
                        FormaEntrega = formaentrega,
                        Mercaderias = MercaderiaResult.ToList()
                    });
                
            }
            return result.ToList();
        }

        public ResponseGetComandaById GetComandaById(int id)
        {
            var db = new QueryFactory(_connection, _SqlKataCompiler);

            var comanda = db.Query("Comandas")
                .Select("Comandas.ComandaId",
                "Comandas.PrecioTotal",
                "Comandas.Fecha",
                "Comandas.FormaEntregaId")
                .Where("ComandaId", "=", id)
                .FirstOrDefault<ResponseGetComandaById>();

            var formaentrega = db.Query("FormasEntregas")
                .Select("FormaEntregaId", "Descripcion")
                .Where("FormaEntregaId", "=", comanda.FormaEntregaId)
                .FirstOrDefault<ResponseGetComandaByIdFormaEntrega>();

            var mercaderias = db.Query("ComandaMercaderias")
                .Select()
                .Where("ComandaId", "=", id)
                .Join("Mercaderias", "Mercaderias.MercaderiaId", "ComandaMercaderias.MercaderiaId");

            var result = mercaderias.Get<ResponseGetComandaByIdMercaderias>();
            return new ResponseGetComandaById
            {
                 ComandaId = comanda.ComandaId,
                 PrecioTotal = 1000,
                 Fecha = comanda.Fecha,
                 FormaEntrega = formaentrega,
                 Mercaderias = result.ToList()
             };
        }
    }
}
