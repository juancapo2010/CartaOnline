using CartaOnline.DTO;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CartaOnline.Query
{

    public interface IMercaderiaQuery
    {
        List<ResponseGetMercaderiaTipoDto> GetComandaByTipo(string tipo);
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
        public List<ResponseGetMercaderiaTipoDto> GetComandaByTipo(string tipo)
        {
            var db = new QueryFactory(_connection, _SqlKataCompiler);

            var mercaderia = db.Query("Mercaderias")
                .Select("Mercaderias.Nombre","Mercaderias.TipoMercaderiaId as Tipo","Mercaderias.Precio","Mercaderias.Ingredientes","Mercaderias.Preparacion")
                .Join("TipoMercaderia","TipoMercaderia.TipoMercaderiaId","Mercaderias.TipoMercaderiaId")
                .When(!string.IsNullOrWhiteSpace(tipo), q => q.WhereLike("TipoMercaderia.Descripcion", $"%{tipo}%"));

            var result = mercaderia.Get<ResponseGetMercaderiaTipoDto>();
            return result.ToList();

        }
    }
}
