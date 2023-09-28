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

        [HttpGet("{ID}")]
        public async Task<ActionResult> GetFuncion(int ID) 
        {
            FuncionRespuesta funcion = await _ServicioFunciones.GetDatosFuncion(ID);
            if (funcion == null)
            {
                var respuesta = new { Motivo = "El ID ingresado no coincide con ninguna funcion registrada en la base de datos, intente con otra ID." };
                return NotFound(respuesta);
            }
            return Ok(funcion);
        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<FuncionRespuesta>>> GetFunciones(string? Fecha= null,string? Pelicula=null, int? IdGenero =null) 
        {
            try
            {   //paso los parametros a capa de aplicación
                List<FuncionRespuesta> result = await _ServicioFunciones.GetFuncionesRespuesta(Fecha, Pelicula, IdGenero);
                if (result.Count() == 0) //Si no se tienen resultados arroja un mensaje 404
                {
                    var respuesta = new { Motivo = "No se encontraron proximas funciones." };
                    return NotFound(respuesta);
                }
                return new JsonResult(result) { StatusCode = 200 }; //Devuelve los resultados encontrados 
            }
            catch (Exception)
            {
                var respuesta = new { Motivo = "Ingrese la fecha correctamente con formato dd/mm." }; //Único caso permitido por swagger, ingresar una fecha con formato que luego no se pueda convertir en Datetime
                return BadRequest(respuesta);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CrearFunciones(FuncionesDTO funcion)
        {
            List<bool> result = await _ServicioFunciones.GetId(funcion.PeliculaId, funcion.SalaId); //Compruebo que las ID de pelicula y sala existan en la base de datos
            if (result[0] == false) //Si ID de Pelicula no existe 
            {
                var respuesta = new { Motivo = "No existe una pelicula asociada a ese ID" };
                return BadRequest(respuesta);
            }
            if (result[1] == false) //Si ID de Sala no existe
            {
                var respuesta = new { Motivo = "No existe una Sala asociada a ese ID" };
                return BadRequest(respuesta);
            }
            try
            {
                TimeSpan Comprobar2 = DateTime.Parse(funcion.Hora).TimeOfDay; //Comprobamos que la hora ingresada se pueda convertir al tipo datetime
                if (await _ServicioFunciones.ComprobarHorario(funcion.SalaId,funcion.Fecha,Comprobar2)) //Si es true, se devuelve el mensaje HTTP409 que indica que el horario se superpone con otra funcion en la misma sala
                {
                    var respuesta = new { Motivo = "Horario ocupado, por favor ingrese otro" };
                    return Conflict(respuesta);
                }
            }
            catch (FormatException) //Si la hora no se ingresa con formato correcto se arroja un Http400. 
            {
                var respuesta = new { Motivo = "Por favor ingrese un horario valido con formato hh:mm" };
                return BadRequest(respuesta);
            }

            return new JsonResult(await _ServicioFunciones.AddFunciones(funcion)); 
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> RemoveFunciones(int ID) 
        {
            Funciones func = await _ServicioFunciones.ComprobarFunciones(ID); //Compuebo que el ID exista y si existe se retorna la función con dicho ID
            if (func != null) //Si la función existe ingresa
            {
                FuncionEliminadaResponse funcion = await _ServicioFunciones.EliminarFuncion(func); //Mandamos la funcion a eliminar
                if (funcion != null) return Ok(funcion); //Devuelve el response de la función eliminada
                else {
                    var respuest = new { Motivo = "La funcion que desea eliminar ya tiene tickets vendidos por lo que no se puede eliminar."};
                    return Conflict(respuest); //Si no se pudo eliminar la función arroja un mensaje HTTP409
                }
            }//Si la función no existe arroja un mensaje HTTP404
            var respuesta = new { Motivo = "El ID ingresado no corresponde a ninguna Funcion registrada en la base de datos."};
            return NotFound(respuesta); 
        }

        [HttpGet("{ID}/Tickets")]
        public async Task<ActionResult> ComprobarTickets(int ID)
        {
            if (await _ServicioFunciones.ComprobarFunciones(ID) == null) //Comprobamos que la función exista
            {
                var respuesta = new { Motivo = "Función no registrada en la base de datos"}; //Si no existe se arroja un mensaje HTTP404
                return NotFound(respuesta);
            }//Si se encuentra la función devuelve el response de asientos disponibles 
            AsientosRespuesta TicketsDisponibles = await _ServicioSalas.CapacidadDisponible(ID);
            return Ok(TicketsDisponibles);
        }

        [HttpPost("{ID}/Tickets")]
        public async Task<ActionResult> CrearTicket(int ID, TicketDTO Ticket)
        {
            if (await _ServicioFunciones.ComprobarFunciones(ID) == null) //Comprobamos que la función exista
            {
                var respuesta = new { Motivo = "Función no registrada en la base de datos"}; //Si no existe se devuelve un mensaje HTTP404
                return NotFound(respuesta);
            }

            AsientosRespuesta Asientos = await _ServicioSalas.CapacidadDisponible(ID); //Se consulta la capacidad

            if (Ticket.Cantidad <= 0)
            {
                var respuesta = new { Motivo = "Ingreso de un numero no valido. Por favor ingrese un valor mayor a 0" };
                return BadRequest(respuesta);
            }
            
            if (Asientos.Cantidad < Ticket.Cantidad) //Si se solicitan más entradas de las disponibles arroja un mensaje HTTP400
            {
                var respuesta = new { Motivo = "La cantidad de entradas solicitadas excedes a la cantidad de entradas disponibles"};
                return BadRequest(respuesta);
            }
            //Si existe la cantidad solicitada, se imprimen las entradas con los datos de la función
            return Ok(await _ServicioFunciones.GenerarTicket(ID,Ticket));
        }

    }
}
