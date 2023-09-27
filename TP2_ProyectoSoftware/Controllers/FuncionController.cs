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
                Response.Headers.Add("Motivo", "El ID ingresado no coincide con ninguna funcion registrada en la base de datos, intente con otra ID.");

                return NoContent();
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
                    Response.Headers.Add("Motivo", "No se encontraron proximas funciones.");
                    return NoContent();
                }
                else
                {
                    return new JsonResult(await _ServicioFunciones.GetCartelera(result)) { StatusCode = 200}; //Luego solucionar el "result" (pasar los condicionales a capa de aplicación)
                } //Ver el mensaje que aparece por debajo de la lista de funciones.
            }catch (FormatException)
            {
                return BadRequest("Por favor, ingrese una fecha válida");
            }catch (Exception)
            {
                return BadRequest("Ingrese los datos correctamente");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CrearFunciones(FuncionesDTO funcion)
        {
            List<FuncionCompletaRespuesta> Retorno = new List<FuncionCompletaRespuesta>();
            List<bool> result = await _ServicioFunciones.GetId(funcion.PeliculaId, funcion.SalaId);
            if (result[0] == false) return BadRequest("No existe una pelicula asociada a ese ID");
            if (result[1] == false) return BadRequest("No existe una Sala asociada a ese ID");
            try
            {
                TimeSpan Comprobar2 = DateTime.Parse(funcion.Hora).TimeOfDay;
            if (await _ServicioFunciones.ComprobarHorario(funcion.SalaId,funcion.Fecha,Comprobar2))
            {
                return BadRequest("Horario ocupado, por favor ingrese otro");
            }
            }
            catch (FormatException)
            {
                return BadRequest("Por favor ingrese una fecha y/o día valido");
            }

            return new JsonResult(await _ServicioFunciones.AddFunciones(funcion));
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> RemoveFunciones(int ID) 
        {
            Funciones func = await _ServicioFunciones.ComprobarFunciones(ID);
            if (func != null)
            {
                EliminarFuncionResponse funcion = await _ServicioFunciones.EliminarFuncion(func);
                if (funcion != null) return Ok(funcion);
                else return BadRequest("La funcion que desea eliminar ya tiene tickets vendidos por lo que no se puede eliminar.");
            }
             Response.Headers.Add("Motivo", "El ID ingresado no corresponde a ninguna Funcion registrada en la base de datos.");
            return NoContent(); 
        }

        [HttpGet("{ID}/Tickets")]
        public async Task<ActionResult> ComprobarTickets(int ID)
        {
            if (await _ServicioFunciones.ComprobarFunciones(ID) == null)
            {
                return NotFound("Función no registrada en la base de datos");
            }

            AsientosResponse TicketsDisponibles = await _ServicioSalas.CapacidadDisponible(ID);

            if (TicketsDisponibles.Cantidad > 0)
            {
                return Ok(TicketsDisponibles);
            }
            Response.Headers.Add("Motivo", "Lo sentimos, ya no quedan entradas");
            return NoContent();
        }

        [HttpPost("{ID}/Tickets")]
        public async Task<ActionResult> CrearTicket(int ID, TicketDTO Ticket)
        {
            if (await _ServicioFunciones.ComprobarFunciones(ID) == null) //Comprobar que es menos costoso comprobar y devolver un booleano
            {
                return NotFound("Función no registrada en la base de datos");
            }

            AsientosResponse Asientos = await _ServicioSalas.CapacidadDisponible(ID);

            if (Asientos.Cantidad < 1)
            {
                Response.Headers.Add("Motivo", "No quedan más entradas disponibles");
                return NoContent();
            }

            return Ok(await _ServicioFunciones.GenerarTicket(ID,Ticket));
        }

    }
}
