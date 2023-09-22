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
            try
            {
                Funciones func = new Funciones
                {
                    PeliculaId = funcion.PeliculaId, //Ir a buscar las ID, comparar si existen y por otro lado, actuar al respecto.
                    SalaId = funcion.SalaId,
                    Fecha = DateTime.Parse(funcion.Fecha), //Controlar errores temporales
                    Hora = DateTime.Parse(funcion.Hora),
                };
                await _Servicio.AddFunciones(func);
            }
            catch (FormatException)
            {
                return StatusCode(500);
            }
            return new JsonResult(funcion);
        }

    }
}
