using Microsoft.AspNetCore.Mvc;
using CartaOnline.Models;
using CartaOnline.Services;
using CartaOnline.DTO;
using System.Web.Http.Cors;

namespace CartaOnline.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaMercaderiaController : ControllerBase
    {
        private readonly IComandaMercaderiaervice _service;

        public ComandaMercaderiaController(IComandaMercaderiaervice service)
        {
            _service = service;
        }

        // POST: api/ComandaMercaderia
        [HttpPost]
        public ComandaMercaderia PostComandaMercaderia(ComandaMercaderiaDto comandaMercaderia)
        {
          return _service.AgregarMercaderia(comandaMercaderia);

        }



    }
}
