using Microsoft.AspNetCore.Mvc;
using CartaOnline.Models;
using CartaOnline.Services;
using CartaOnline.DTO;

namespace CartaOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaMercaderiasController : ControllerBase
    {
        private readonly IComandaMercaderiaService _service;

        public ComandaMercaderiasController(IComandaMercaderiaService service)
        {
            _service = service;
        }

        // POST: api/ComandaMercaderias
        [HttpPost]
        public ComandaMercaderia PostComandaMercaderia(ComandaMercaderiaDto comandaMercaderia)
        {
          return _service.AgregarMercaderia(comandaMercaderia);

        }



    }
}
