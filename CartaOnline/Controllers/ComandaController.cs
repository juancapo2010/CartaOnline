using System;
using Microsoft.AspNetCore.Mvc;
using CartaOnline.Services;
using CartaOnline.DTO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace CartaOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly IComandaService _service;

        public ComandaController(IComandaService service)
        {
            _service = service;
        }

        // GET: api/Comanda
        [HttpGet]
        [ProducesResponseType(typeof(List<ResponseGetAllComandaDto>), StatusCodes.Status200OK)]
        public IActionResult GetComanda([FromQuery] string hora)
        {
            try
            {
                return new JsonResult(_service.GetComanda(hora)) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Comanda/5
        [HttpGet("{id}")]
        public IActionResult GetComanda(int id)
        {
            try
            {         
                return new JsonResult(_service.GetComandaId(id)) { StatusCode = 200 };
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        // POST: api/Comanda
        [HttpPost]
        public IActionResult PostComanda(ComandaDto comanda)
        {
            try
            {
                return new JsonResult(_service.CreateComanda(comanda)) { StatusCode = 201 };
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
