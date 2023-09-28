using Aplicacion.DTO;
using Aplicacion.Interfaces.Aplicacion;
using Aplicacion.RespuestasHTTP;
using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        [HttpGet("{ID}")]
        public async Task<ActionResult> GetFuncion(int ID) 
        {
            FuncionCompletaRespuesta funcion = await _ServicioFunciones.GetDatosFuncion(ID);
            if (funcion == null)
            {
                var respuesta = new { Motivo = "El ID ingresado no coincide con ninguna funcion registrada en la base de datos, intente con otra ID." };
                return NotFound(respuesta);
            }
            return Ok(funcion);
        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<FuncionCompletaRespuesta>>> GetFunciones(string? Fecha= null,string? Pelicula=null, int? IdGenero =null) 
        {
            try
            {
                List<FuncionCompletaRespuesta> result = new List<FuncionCompletaRespuesta>();
                bool controlador = true;

                if (Fecha != null && controlador)
                {       
                    DateTime dia= DateTime.Parse(Fecha);
                    result = await _ServicioFunciones.GetFuncionesDia(dia, result);
                    if (result.Count() == 0) controlador = false;
                }

                if (Pelicula != null && controlador)
                {
                    result = await _ServicioFunciones.GetFuncionesNombrePelicula(Pelicula, result);
                    if (result.Count() == 0) controlador = false;
                }

                if (IdGenero != null && controlador)
                {
                    result = await _ServicioFunciones.GetFuncionesGenero(IdGenero, result);
                    if (result.Count() == 0) controlador = false;
                }

                if (Fecha == null && Pelicula == null && IdGenero == null)
                {
                    result = await _ServicioFunciones.GetFuncionesRespuesta();
                }

                if (result.Count() == 0)
                {
                    var respuesta = new { Motivo = "No se encontraron proximas funciones." };
                    return NotFound(respuesta);
                }
                else
                {
                    return new JsonResult(await _ServicioFunciones.GetCartelera(result)) { StatusCode = 200}; 
                } 
            }catch (FormatException)
            {
                var respuesta = new { Motivo = "Por favor, ingrese una fecha válida." };
                return BadRequest(respuesta);
            }catch (Exception)
            {
                var respuesta = new { Motivo = "Ingrese los datos correctamente." };
                return BadRequest(respuesta);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CrearFunciones(FuncionesDTO funcion)
        {
            List<bool> result = await _ServicioFunciones.GetId(funcion.PeliculaId, funcion.SalaId);
            if (result[0] == false)
            {
                var respuesta = new { Motivo = "No existe una pelicula asociada a ese ID" };
                return BadRequest(respuesta);
            }
            if (result[1] == false)
            {
                var respuesta = new { Motivo = "No existe una Sala asociada a ese ID" };
                return BadRequest(respuesta);
            }
            try
            {
                TimeSpan Comprobar2 = DateTime.Parse(funcion.Hora).TimeOfDay;
                if (await _ServicioFunciones.ComprobarHorario(funcion.SalaId,funcion.Fecha,Comprobar2))
                {
                    var respuesta = new { Motivo = "Horario ocupado, por favor ingrese otro" };
                    return Conflict(respuesta);
                }
            }
            catch (FormatException)
            {
                var respuesta = new { Motivo = "Por favor ingrese una fecha y/o día valido" };
                return BadRequest(respuesta);
            }

            return new JsonResult(await _ServicioFunciones.AddFunciones(funcion)); //Arreglar el mal ingreso del formato horario
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> RemoveFunciones(int ID) 
        {
            Funciones func = await _ServicioFunciones.ComprobarFunciones(ID);
            if (func != null)
            {
                EliminarFuncionResponse funcion = await _ServicioFunciones.EliminarFuncion(func);
                if (funcion != null) return Ok(funcion);
                else {
                    var respuest = new { Motivo = "La funcion que desea eliminar ya tiene tickets vendidos por lo que no se puede eliminar."};
                    return Conflict(respuest); 
                }
            }
            var respuesta = new { Motivo = "El ID ingresado no corresponde a ninguna Funcion registrada en la base de datos."};
            return NotFound(respuesta); 
        }

        [HttpGet("{ID}/Tickets")]
        public async Task<ActionResult> ComprobarTickets(int ID)
        {
            if (await _ServicioFunciones.ComprobarFunciones(ID) == null)
            {
                var respuesta = new { Motivo = "Función no registrada en la base de datos"};
                return NotFound(respuesta);
            }
            AsientosResponse TicketsDisponibles = await _ServicioSalas.CapacidadDisponible(ID);
            return Ok(TicketsDisponibles);
        }

        [HttpPost("{ID}/Tickets")]
        public async Task<ActionResult> CrearTicket(int ID, TicketDTO Ticket)
        {
            if (await _ServicioFunciones.ComprobarFunciones(ID) == null) 
            {
                var respuesta = new { Motivo = "Función no registrada en la base de datos"};
                return NotFound(respuesta);
            }

            AsientosResponse Asientos = await _ServicioSalas.CapacidadDisponible(ID);

            if (Asientos.Cantidad < Ticket.Cantidad)
            {
                var respuesta = new { Motivo = "La cantidad de entradas solicitadas excedes a la cantidad de entradas disponibles"};
                return BadRequest(respuesta);
            }

            return Ok(await _ServicioFunciones.GenerarTicket(ID,Ticket));
        }

    }
}
