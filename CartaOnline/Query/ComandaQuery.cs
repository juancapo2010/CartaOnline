using CartaOnline.DTO;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace CartaOnline.Query
{
    public interface IComandaQuery
    {
        List<ResponseGetAllComandaDto> GetAllComanda(string hora);
        ResponseGetComandaById GetComandaById(int id);

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

        public List<ResponseGetAllComandaDto> GetAllComanda(string hora)
        {
            try
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
                        .Where("ComandaId", "=", c.ComandaId)
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
            }catch
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }
            
        }

        public ResponseGetComandaById GetComandaById(int id)
        {
            try
            {
                var db = new QueryFactory(_connection, _SqlKataCompiler);

                var comanda = db.Query("Comanda")
                    .Select("Comanda.ComandaId",
                    "Comanda.PrecioTotal",
                    "Comanda.Fecha",
                    "Comanda.FormaEntregaId")
                    .Where("ComandaId", "=", id)
                    .FirstOrDefault<ResponseGetComandaById>();
         
                if (comanda != null)
                {
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
                        PrecioTotal = comanda.PrecioTotal,
                        Fecha = comanda.Fecha,
                        FormaEntrega = formaentrega,
                        Mercaderia = result.ToList()
                    };
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

        }
    }
}
