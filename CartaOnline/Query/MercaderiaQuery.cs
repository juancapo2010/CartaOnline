using CartaOnline.DTO;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace CartaOnline.Query
{

    public interface IMercaderiaQuery
    {
        List<ResponseGetMercaderiaTipoDto> GetMercaderiaByTipo(string tipo);
    }
    public class MercaderiaQuery : IMercaderiaQuery
    {
        private readonly IDbConnection _connection;
        private readonly Compiler _SqlKataCompiler;
        public MercaderiaQuery(IDbConnection connection, Compiler SqlKataCompiler)
        {
            _connection = connection;
            _SqlKataCompiler = SqlKataCompiler;
        }
        public List<ResponseGetMercaderiaTipoDto> GetMercaderiaByTipo(string tipo)
        {
            try
            {
                var db = new QueryFactory(_connection, _SqlKataCompiler);

                var mercaderia = db.Query("Mercaderias")
                    .Select("Mercaderias.MercaderiaId as id", "Mercaderias.Nombre", "Mercaderias.TipoMercaderiaId as Tipo", "Mercaderias.Precio", "Mercaderias.Ingredientes", "Mercaderias.Preparacion", "Mercaderias.Imagen")
                    .Join("TipoMercaderia", "TipoMercaderia.TipoMercaderiaId", "Mercaderias.TipoMercaderiaId")
                    .When(!string.IsNullOrWhiteSpace(tipo),
                        q => q.WhereLike("TipoMercaderia.TipoMercaderiaId", $"%{tipo}%"));
                int cont =0;
                foreach(var validar in mercaderia.ToString())
                {
                    cont = cont + 1;
                }
                if(cont >0)
                {
                    
                    var result = mercaderia.Get<ResponseGetMercaderiaTipoDto>();
                    return result.ToList();
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            

        }
    }
}
