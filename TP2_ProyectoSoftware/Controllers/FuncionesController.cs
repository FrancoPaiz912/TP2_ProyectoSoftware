using Aplicacion.DTO;
using Aplicacion.Interfaces.Aplicacion;
using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TP2_ProyectoSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionesController : ControllerBase
    {
        private readonly IServiciosFunciones _Servicio;
        
        public FuncionesController(IServiciosFunciones Servicio)
        {
            _Servicio = Servicio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cartelera>>> GetFunciones(string? Fecha= null,int? IdPelicula=null, int? IdGenero =null) 
        {
            try
            {
                List<CarteleraDTO> result = new List<CarteleraDTO>();
                bool controlador = true;

                if (Fecha != null && controlador)
                {       
                    DateTime dia= DateTime.Parse(Fecha);
                    result = await _Servicio.GetFuncionesDia(dia, result);
                    if (result.Count() == 0) controlador = false;
                }

                if (IdPelicula != null && controlador)
                {
                    result = await _Servicio.GetFuncionesNombrePelicula(IdPelicula, result);
                    if (result.Count() == 0) controlador = false;
                }

                if (IdGenero != null && controlador)
                {
                    result = await _Servicio.GetFuncionesGenero(IdGenero, result);
                    if (result.Count() == 0) controlador = false;
                }

                if (Fecha == null && IdPelicula == null && IdGenero == null)
                {
                    result = await _Servicio.GetFunciones();
                }

                if (!result.Any())
                {
                    return NotFound("No se encontraron proximas funciones");
                }
                else
                {
                    List<Cartelera> cartelera = new List<Cartelera>();
                    foreach (var item in result)
                    {
                        cartelera.Add(new Cartelera
                        {
                            Titulo = item.Titulo,
                            Sinopsis = item.Sinopsis,
                            Poster = item.Poster,
                            Trailer = item.Trailer,
                            Sala = item.Sala,
                            Capacidad = item.Capacidad,
                            Fecha = item.Fecha.Date.ToString("dd/MM/yyyy"),
                            Hora = item.Hora.ToString("HH:mm"),
                            genero = item.genero,
                        });
                    }
                    return Ok(cartelera);
                }
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
            List<bool> result = await _Servicio.GetId(funcion.PeliculaId, funcion.SalaId);
            if (result[0] == false) return BadRequest("No existe una pelicula asociada a ese ID");
            if (result[1] == false) return BadRequest("No existe una Sala asociada a ese ID");
            try
            {
                DateTime Comprobar1 = DateTime.Parse(funcion.Fecha).Date;
                TimeSpan Comprobar2 = DateTime.Parse(funcion.Hora).TimeOfDay;
            if (await _Servicio.ComprobarHorario(funcion.SalaId,Comprobar1,Comprobar2))
            {
                return BadRequest("Horario ocupado, por favor ingrese otro");
            }
            }
            catch (FormatException)
            {
                return BadRequest("Por favor ingrese una fecha y/o día valido");
            }
            Funciones func = new Funciones
                {
                    PeliculaId = funcion.PeliculaId,
                    SalaId = funcion.SalaId,
                    Fecha = DateTime.Parse(funcion.Fecha),  //Ver tiempo de función. Ir a: Fecha, Sala, Comprobar (beetween (where)) si el horario esta en el medio de funciones
                    Hora = DateTime.Parse(funcion.Hora).TimeOfDay,
                };
            await _Servicio.AddFunciones(func);
            return new JsonResult(funcion);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveFunciones(int ID)
        {
            Funciones func = await _Servicio.ComprobarFunciones(ID);
            if (func != null)
            {
                await _Servicio.EliminarFuncion(func);
                return Ok("La funcion ha sido borrada exitosamente.");
            }
            else return NotFound("El ID ingresado no corresponde a ninguna Funcion registrada en la base de datos."); 
        }
    }
}
