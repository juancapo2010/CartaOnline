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
    public class MercaderiaController : ControllerBase
    {
        private readonly IMercaderiaService _service;

        public MercaderiaController(IMercaderiaService service)
        {
            _service = service;
        }

        // GET: api/Mercaderia
        [HttpGet]
        [ProducesResponseType(typeof(List<ResponseGetMercaderiaTipoDto>), StatusCodes.Status200OK)]
        public IActionResult GetMercaderia([FromQuery] string tipo)
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

        // GET: api/Mercaderia/5
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

        // PUT: api/Mercaderia/5
        [HttpPut("{id}")]
        public IActionResult PutMercaderia(int id, MercaderiaUpdateDto mercaderia)
        {
            try
            {
                return new JsonResult(_service.UpdateMercaderia(id,mercaderia)) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }

        // POST: api/Mercaderia
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

        // DELETE: api/Mercaderia/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult DeleteMercaderia(int id)
        {
            try
            {
                var mercaderia = _service.GetMercaderiaId(id);
                if(mercaderia!=null) {
                    _service.DeleteMercaderiaId(id);
                    return new JsonResult(mercaderia) { StatusCode = 200 };
                }
                else
                {
                    return new JsonResult(mercaderia) { StatusCode = 404 };
                }
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }


        }

    }
}
