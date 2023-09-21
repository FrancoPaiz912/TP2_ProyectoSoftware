using Aplicacion.DTO;
using Aplicacion.Interfaces.Aplicacion;
using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<Cartelera>>> GetFunciones(DateTime? dia= null,int? IdPelicula=null, int? IdGenero =null) 
        {
            List<FuncionesDTO> result = new List<FuncionesDTO>();

            if (dia != null)
            {
                result = await _Servicio.GetFuncionesDia(dia,result);
            }
            
            if (IdPelicula != null)
            {
                result = await _Servicio.GetFuncionesNombrePelicula(IdPelicula, result);
            }
            
            if (IdGenero != null)
            {
                result = await _Servicio.GetFuncionesGenero(IdGenero, result);
            }

            if (dia == null && IdPelicula == null && IdGenero == null)
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
                    cartelera.Add(new Cartelera{
                        Titulo = item.Titulo,
                        Sinopsis = item.Sinopsis,
                        Poster = item.Poster,
                        Trailer = item.Trailer,
                        Sala = item.Sala,
                        Capacidad = item.Capacidad,
                        Fecha = item.Fecha,
                        Hora = item.Hora,
                        genero = item.genero,
                    });
                }
                return Ok(cartelera);
            }


        }
    }
}
