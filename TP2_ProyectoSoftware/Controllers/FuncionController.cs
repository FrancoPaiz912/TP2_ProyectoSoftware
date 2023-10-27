using Aplicacion.DTO;
using Aplicacion.Interfaces.Aplicacion;
using Aplicacion.RespuestasHTTP;
using Dominio;
using Microsoft.AspNetCore.Mvc;
namespace TP2_ProyectoSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionController : ControllerBase
    {
        private readonly IServiciosFunciones _ServicioFunciones;
        private readonly IServiciosSalas _ServicioSalas;

        public FuncionController(IServiciosFunciones funciones, IServiciosSalas salas)
        {
            _ServicioFunciones = funciones;
            _ServicioSalas = salas;
        }

        [HttpGet("/api/v1/Funcion/{id}")]
        public async Task<ActionResult> GetFuncion(int id)
        {
            if (id <= 0)
            {
                var respuesta = new { Motivo = "El ID ingresado no es valido, ingrese un id mayor a 0" };
                return BadRequest(respuesta);
            }

            FuncionRespuesta funcion = await _ServicioFunciones.GetDatosFuncion(id);
            if (funcion == null)
            {
                var respuesta = new { Motivo = "El ID ingresado no coincide con ninguna funcion registrada en la base de datos, intente con otra ID." };
                return NotFound(respuesta);
            }
            return Ok(funcion);
        }

        [HttpGet("/api/v1/Funcion")]
        public async Task<ActionResult<IEnumerable<FuncionRespuesta>>> GetFunciones(string? fecha = null, string? titulo = null, int? genero = null)
        {
            try
            {   //paso los parametros a capa de aplicación
                List<FuncionRespuesta> result = await _ServicioFunciones.GetFuncionesRespuesta(fecha, titulo, genero);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (Exception)
            {
                var respuesta = new { Motivo = "Ingrese la fecha correctamente con formato dd/mm." };
                return BadRequest(respuesta);
            }
        }

        [HttpPost("/api/v1/Funcion")]
        public async Task<ActionResult> CrearFunciones(FuncionesDTO funcion)
        {
            List<bool> result = await _ServicioFunciones.GetId(funcion.pelicula, funcion.sala);
            if (!result[0])
            {
                var respuesta = new { Motivo = "No existe una pelicula asociada a ese ID" };
                return BadRequest(respuesta);
            }
            if (!result[1])
            {
                var respuesta = new { Motivo = "No existe una Sala asociada a ese ID" };
                return BadRequest(respuesta);
            }
            try
            {
                TimeSpan Comprobar2 = DateTime.Parse(funcion.horario).TimeOfDay;
                if (await _ServicioFunciones.ComprobarHorario(funcion.sala, funcion.fecha, Comprobar2))
                {
                    var respuesta = new { Motivo = "Horario ocupado, por favor ingrese otro" };
                    return Conflict(respuesta);
                }
            }
            catch (FormatException)
            {
                var respuesta = new { Motivo = "Por favor ingrese un horario valido con formato hh:mm" };
                return BadRequest(respuesta);
            }

            return new JsonResult(await _ServicioFunciones.AddFunciones(funcion)) { StatusCode = 201 };
        }

        [HttpDelete("/api/v1/Funcion/{id}")]
        public async Task<ActionResult> RemoveFunciones(int id)
        {
            if (id <= 0)
            {
                var response = new { Motivo = "El ID ingresado no es valido, ingrese un id mayor a 0" };
                return BadRequest(response);
            }

            Funciones func = await _ServicioFunciones.ComprobarFunciones(id);
            if (func != null)
            {
                FuncionEliminadaResponse funcion = await _ServicioFunciones.EliminarFuncion(func);
                if (funcion != null) return Ok(funcion);
                else
                {
                    var respuest = new { Motivo = "La funcion que desea eliminar ya tiene tickets vendidos por lo que no se puede eliminar." };
                    return Conflict(respuest);
                }
            }
            var respuesta = new { Motivo = "El ID ingresado no corresponde a ninguna Funcion registrada en la base de datos." };
            return NotFound(respuesta);
        }

        [HttpGet("/api/v1/Funcion/{id}/tickets")]
        public async Task<ActionResult> ComprobarTickets(int id)
        {
            if (id <= 0)
            {
                var respuesta = new { Motivo = "El ID ingresado no es valido, ingrese un id mayor a 0" };
                return BadRequest(respuesta);
            }

            if (await _ServicioFunciones.ComprobarFunciones(id) == null)
            {
                var respuesta = new { Motivo = "Función no registrada en la base de datos" };
                return NotFound(respuesta);
            }
            AsientosRespuesta TicketsDisponibles = await _ServicioSalas.CapacidadDisponible(id);
            return Ok(TicketsDisponibles);
        }

        [HttpPost("/api/v1/Funcion/{id}/tickets")]
        public async Task<ActionResult> CrearTicket(int id, TicketDTO Ticket)
        {
            if (await _ServicioFunciones.ComprobarFunciones(id) == null) 
            {
                var respuesta = new { Motivo = "Función no registrada en la base de datos" }; 
                return NotFound(respuesta);
            }

            AsientosRespuesta Asientos = await _ServicioSalas.CapacidadDisponible(id); 

            if (Ticket.Cantidad <= 0)
            {
                var respuesta = new { Motivo = "Ingreso de un numero no valido. Por favor ingrese un valor mayor a 0" };
                return BadRequest(respuesta);
            }

            if (Asientos.Cantidad < Ticket.Cantidad) 
            {
                var respuesta = new { Motivo = "La cantidad de entradas solicitadas excedes a la cantidad de entradas disponibles" };
                return BadRequest(respuesta);
            }
           
            return Ok(await _ServicioFunciones.GenerarTicket(id, Ticket));
        }

    }
}
