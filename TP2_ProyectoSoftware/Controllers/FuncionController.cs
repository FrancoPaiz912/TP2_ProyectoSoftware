using Aplicacion.DTO;
using Aplicacion.Interfaces.Aplicacion;
using Aplicacion.RespuestasHTTP;
using Dominio;
using Microsoft.AspNetCore.Mvc;
namespace TP2_ProyectoSoftware.Controllers
{
    [Route("api/v1/[controller]")]
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

        [HttpGet]
        [ProducesResponseType(typeof(FuncionRespuesta), 200)]
        [ProducesResponseType(typeof(Mensaje), 400)]
        public async Task<ActionResult<IEnumerable<FuncionRespuesta>>> GetFunciones(string? fecha = null, string? titulo = null, int? genero = null)
        {
            try
            {
                List<FuncionRespuesta> result = await _ServicioFunciones.GetFuncionesRespuesta(fecha, titulo, genero);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (Exception)
            {
                Mensaje respuesta = new Mensaje();
                respuesta.Message = "Ingrese la fecha correctamente con formato dd/mm.";
                return BadRequest(respuesta.Message);
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FuncionRespuesta), 200)]
        [ProducesResponseType(typeof(Mensaje), 404)]
        [ProducesResponseType(typeof(Mensaje), 400)]

        public async Task<ActionResult> GetFuncion(int id)
        {
            if (id <= 0)
            {
                Mensaje respuesta = new Mensaje();
                respuesta.Message = "El ID ingresado no es valido, ingrese un id mayor a 0";
                return BadRequest(respuesta.Message);
            }

            FuncionRespuesta funcion = await _ServicioFunciones.GetDatosFuncion(id);
            if (funcion == null)
            {
                Mensaje respuesta = new Mensaje();
                respuesta.Message = "El ID ingresado no coincide con ninguna funcion registrada en la base de datos, intente con otra ID.";
                return NotFound(respuesta.Message);
            }
            return Ok(funcion);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FuncionRespuesta), 201)]
        [ProducesResponseType(typeof(Mensaje), 400)]
        [ProducesResponseType(typeof(Mensaje), 409)]
        public async Task<ActionResult> CrearFunciones(FuncionesDTO funcion)
        {
            List<bool> result = await _ServicioFunciones.GetId(funcion.pelicula, funcion.sala);
            if (!result[0])
            {
                Mensaje respuesta = new Mensaje();
                respuesta.Message = "No existe una pelicula asociada a ese ID.";
                return BadRequest(respuesta.Message);
            }
            if (!result[1])
            {
                Mensaje respuesta = new Mensaje();
                respuesta.Message = "No existe una Sala asociada a ese ID.";
                return BadRequest(respuesta.Message);
            }
            try
            {
                TimeSpan Comprobar2 = DateTime.Parse(funcion.horario).TimeOfDay;
                if (await _ServicioFunciones.ComprobarHorario(funcion.sala, funcion.fecha, Comprobar2))
                {
                    Mensaje respuesta = new Mensaje();
                    respuesta.Message = "Horario ocupado, por favor ingrese otro.";
                    return Conflict(respuesta.Message);
                }
            }
            catch (FormatException)
            {
                Mensaje respuesta = new Mensaje();
                respuesta.Message = "Por favor ingrese un horario valido con formato hh:mm.";
                return BadRequest(respuesta.Message);
            }

            return new JsonResult(await _ServicioFunciones.AddFunciones(funcion)) { StatusCode = 201 };
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(FuncionEliminadaResponse), 200)]
        [ProducesResponseType(typeof(Mensaje), 400)]
        [ProducesResponseType(typeof(Mensaje), 404)]
        [ProducesResponseType(typeof(Mensaje), 409)]
        public async Task<ActionResult> RemoveFunciones(int id)
        {
            if (id <= 0)
            {
                Mensaje response = new Mensaje();
                response.Message = "El ID ingresado no es valido, ingrese un id mayor a 0.";
                return BadRequest(response.Message);
            }

            Funciones func = await _ServicioFunciones.ComprobarFunciones(id);
            if (func != null)
            {
                FuncionEliminadaResponse funcion = await _ServicioFunciones.EliminarFuncion(func);
                if (funcion != null) return Ok(funcion);
                else
                {
                    Mensaje response = new Mensaje();
                    response.Message = "La funcion que desea eliminar ya tiene tickets vendidos por lo que no se puede eliminar.";
                    return Conflict(response.Message);
                }
            }
            Mensaje respuesta = new Mensaje();
            respuesta.Message = "El ID ingresado no corresponde a ninguna Funcion registrada en la base de datos.";
            return NotFound(respuesta.Message);
        }

        [HttpGet("{id}/tickets")]
        [ProducesResponseType(typeof(AsientosRespuesta), 200)]
        [ProducesResponseType(typeof(Mensaje), 400)]
        [ProducesResponseType(typeof(Mensaje), 404)]
        public async Task<ActionResult> ComprobarTickets(int id)
        {
            if (id <= 0)
            {
                Mensaje respuesta = new Mensaje();
                respuesta.Message = "El ID ingresado no es valido, ingrese un id mayor a 0.";
                return BadRequest(respuesta.Message);
            }

            if (await _ServicioFunciones.ComprobarFunciones(id) == null)
            {
                Mensaje respuesta = new Mensaje();
                respuesta.Message = "Función no registrada en la base de datos.";
                return NotFound(respuesta.Message);
            }
            AsientosRespuesta TicketsDisponibles = await _ServicioSalas.CapacidadDisponible(id);
            return Ok(TicketsDisponibles);
        }

        [HttpPost("{id}/tickets")]
        [ProducesResponseType(typeof(TicketRespuesta), 200)]
        [ProducesResponseType(typeof(Mensaje), 400)]
        [ProducesResponseType(typeof(Mensaje), 404)]
        public async Task<ActionResult> CrearTicket(int id, TicketDTO Ticket)
        {
            if (await _ServicioFunciones.ComprobarFunciones(id) == null)
            {
                Mensaje respuesta = new Mensaje();
                respuesta.Message = "Función no registrada en la base de datos.";
                return NotFound(respuesta.Message);
            }

            AsientosRespuesta Asientos = await _ServicioSalas.CapacidadDisponible(id);

            if (Ticket.Cantidad <= 0)
            {
                Mensaje respuesta = new Mensaje();
                respuesta.Message = "Ingreso de un numero no valido. Por favor ingrese un valor mayor a 0.";
                return BadRequest(respuesta.Message);
            }

            if (Asientos.Cantidad < Ticket.Cantidad)
            {
                Mensaje respuesta = new Mensaje();
                respuesta.Message = "La cantidad de entradas solicitadas excedes a la cantidad de entradas disponibles.";
                return BadRequest(respuesta.Message);
            }

            return Ok(await _ServicioFunciones.GenerarTicket(id, Ticket));
        }

    }
}
