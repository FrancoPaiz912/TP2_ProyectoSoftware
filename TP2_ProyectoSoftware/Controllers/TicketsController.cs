using Aplicacion.DTO;
using Aplicacion.Interfaces.Aplicacion;
using Aplicación.Interfaces.Infraestructura;
using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        //Debe permitir obtener un ticket para una función.
        //Se debe de crear un ticket (Post) y relacionarlo a la función y mediante esta restar capacidad en la sala
        //Pasos
        //1. Comprobar que exista la función (ID). Listo
        //2. Comprobar que haya capacidad disponible en la sala. Para esto comprobamos con la funcion la cantidad de tickets vendidos y se lo descontamos a la capacidad.
        //3. Generar un ticket correctamente. //Posee función id y usuario. Mediante la función ID debemos de buscar y crear un ticket para dicha funcion.

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
