﻿using CartaOnline.DTO;
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
        List<ResponseGetAllComandaDto> GetAllComanda(string hora);
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
            var id = db.Query("Comanda").InsertGetId<int>(
                new
                {
                    comanda.FormaEntregaId,
                    Fecha = DateTime.Now,
                    PrecioTotal = 0
                }
                );
            foreach(var Mercaderia in comanda.Mercaderia.ToList())
            {
                db.Query("ComandaMercaderias").Insert(
                new
                {
                    ComandaId = id,
                    Mercaderia.MercaderiaId
                }
                );
                
            }
            int suma = 0;
            foreach (var actualizapreciototal in comanda.Mercaderia.ToList())
            {
                var actualizo = db.Query("Mercaderias")
                    .Select()
                    .Where("MercaderiaId", "=", actualizapreciototal.MercaderiaId)
                    .FirstOrDefault<ResponseGetComandaByIdMercaderia>();
                 suma = actualizo.Precio + suma;

            }
            db.Query("Comanda").Where("ComandaId", "=", id).Update(new { precioTotal = suma });
            return comanda;
        }

        public List<ResponseGetAllComandaDto> GetAllComanda(string hora)
        {
            var db = new QueryFactory(_connection, _SqlKataCompiler);

            var comanda = db.Query("Comanda")
                .Select("Comanda.ComandaId",
                "Comanda.PrecioTotal",
                "Comanda.Fecha",
                "Comanda.FormaEntregaId")
                .OrderByDesc("Fecha")
                .When(!string.IsNullOrWhiteSpace(hora), q => q.WhereLike("Comanda.Fecha", $"%{hora}%"));
            var ComandaResult = comanda.Get<ResponseGetAllComandaDto>();

            List<ResponseGetAllComandaDto> result = new List<ResponseGetAllComandaDto>();
            foreach (var c in ComandaResult)
            {
                var formaentrega = db.Query("FormasEntregas")
                    .Select("FormaEntregaId", "Descripcion")
                    .Where("FormaEntregaId", "=", c.FormaEntregaId)
                    .FirstOrDefault<ResponseGetFormaEntregaByComanda>();

                var Mercaderia = db.Query("ComandaMercaderias")
                    .Select()
                    .Where("ComandaId", "=",c.ComandaId )
                    .Join("Mercaderias", "Mercaderias.MercaderiaId", "ComandaMercaderias.MercaderiaId");

                var MercaderiaResult = Mercaderia.Get<ResponseGetMercaderiaByComanda>();

                result.Add(
                     new ResponseGetAllComandaDto
                    {
                        ComandaId = c.ComandaId,
                        PrecioTotal = c.PrecioTotal,
                        Fecha = c.Fecha,
                        FormaEntrega = formaentrega,
                        Mercaderia = MercaderiaResult.ToList()
                    });
                
            }
            return result.ToList();
        }

        public ResponseGetComandaById GetComandaById(int id)
        {
            var db = new QueryFactory(_connection, _SqlKataCompiler);

            var comanda = db.Query("Comanda")
                .Select("Comanda.ComandaId",
                "Comanda.PrecioTotal",
                "Comanda.Fecha",
                "Comanda.FormaEntregaId")
                .Where("ComandaId", "=", id)
                .FirstOrDefault<ResponseGetComandaById>();

            var formaentrega = db.Query("FormasEntregas")
                .Select("FormaEntregaId", "Descripcion")
                .Where("FormaEntregaId", "=", comanda.FormaEntregaId)
                .FirstOrDefault<ResponseGetComandaByIdFormaEntrega>();

            var Mercaderia = db.Query("ComandaMercaderias")
                .Select()
                .Where("ComandaId", "=", id)
                .Join("Mercaderias", "Mercaderias.MercaderiaId", "ComandaMercaderias.MercaderiaId");

            var result = Mercaderia.Get<ResponseGetComandaByIdMercaderia>();
            return new ResponseGetComandaById
            {
                 ComandaId = comanda.ComandaId,
                 PrecioTotal =  comanda.PrecioTotal,
                 Fecha = comanda.Fecha,
                 FormaEntrega = formaentrega,
                 Mercaderia = result.ToList()
             };
        }
    }
}
