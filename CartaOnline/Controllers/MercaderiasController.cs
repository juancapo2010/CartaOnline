using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CartaOnline.Services;
using CartaOnline.DTO;

namespace CartaOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MercaderiasController : ControllerBase
    {
        private readonly IMercaderiaService _service;

        public MercaderiasController(IMercaderiaService service)
        {
            _service = service;
        }

        // GET: api/Mercaderias
        [HttpGet]
        [ProducesResponseType(typeof(List<ResponseGetMercaderiaTipoDto>), StatusCodes.Status200OK)]
        public IActionResult GetMercaderias([FromQuery] string tipo)
        {
            try
            {
                return new JsonResult(_service.GetMercaderiaByTipo(tipo)) { StatusCode = 200 };
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        // GET: api/Mercaderias/5
        [HttpGet("{id}")]
        public IActionResult GetMercaderia(int id)
        {
            try
            {
                return new JsonResult(_service.GetMercaderiaId(id)) { StatusCode = 200 };
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        // PUT: api/Mercaderias/5
        [HttpPut("{id}")]
        public IActionResult PutMercaderia(int id, MercaderiaUpdateDto mercaderia)
        {
            try
            {
                return new JsonResult(_service.UpdateMercaderia(mercaderia)) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }

        // POST: api/Mercaderias
        [HttpPost]
        public IActionResult PostMercaderia(MercaderiaDto mercaderia)
        {
            try
            {
                return new JsonResult(_service.CreateMercaderia(mercaderia)) { StatusCode = 201 };
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
       
            }
                
           
        }

        // DELETE: api/Mercaderias/5
        [HttpDelete("{id}")]
        public void DeleteMercaderia(int id)
        {
            _service.DeleteMercaderiaId(id);
        }

    }
}
