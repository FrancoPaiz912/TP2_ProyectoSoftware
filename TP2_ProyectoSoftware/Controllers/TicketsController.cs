using Aplicacion.DTO;
using Aplicacion.Interfaces.Aplicacion;
using Aplicación.Interfaces.Infraestructura;
using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace TP2_ProyectoSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IServiciosFunciones _ServicioFunciones;
        private readonly IServiciosSalas _ServicioSalas;

        public TicketsController(IServiciosFunciones funciones, IServiciosSalas salas)
        {
            _ServicioFunciones = funciones;
            _ServicioSalas = salas;
        }

        [HttpGet("{ID_Funcion}")]
        public async Task<ActionResult> ComprobarTickets(int ID_Funcion)
        {
            Funciones func = await _ServicioFunciones.ComprobarFunciones(ID_Funcion);
            if (func == null)
            {
                return NotFound("Función no registrada en la base de datos");
            }

            int TicketsDisponibles = await _ServicioSalas.CapacidadDisponible(ID_Funcion);

            if (TicketsDisponibles > 0)
            {
                return Ok("Para esta funcion aún quedan " + TicketsDisponibles + " Tickets disponibles");
            }

            return BadRequest("Lo sentimos, ya no quedan entradas");
        }

        [HttpPost]
        public async Task<ActionResult> CrearTicket(TicketDTO Ticket)
        {
            Funciones func = await _ServicioFunciones.ComprobarFunciones(Ticket.FuncionId);
            if (func == null)
            {
                return NotFound("Función no registrada en la base de datos");
            }

            if (await _ServicioSalas.CapacidadDisponible(Ticket.FuncionId) < 1)
            {
                return NotFound("No quedan más entradas disponibles");
            }

            return Ok(await _ServicioFunciones.GenerarTicket(Ticket));
            //return New JSON(func) {StatusCode = 201};
        }
    }
}
