using Aplicacion.DTO;
using Aplicacion.Interfaces.Aplicacion;
using Aplicacion.RespuestasHTTP;
using Microsoft.AspNetCore.Mvc;

namespace TP2_ProyectoSoftware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IServiciosPeliculas _Servicio;

        public PeliculasController(IServiciosPeliculas Servicio)
        {
            _Servicio = Servicio;
        }

        [HttpGet("/api/v1/Pelicula/{id}")]
        public async Task<ActionResult> GetInfoPelicula(int id)
        {
            if (id <= 0)
            {
                var respuesta = new { Motivo = "El ID ingresado no es valido, ingrese un id mayor a 0" };
                return BadRequest(respuesta);
            }

            if (await _Servicio.ComprobarId(id)) 
            { 
                var respuesta = new { Motivo = "El ID ingresado no se encuentra asociado a ninguna pelicula registrada en la base de datos, por favor ingrese uno válido" };
                return NotFound(respuesta);
            }
            PeliculaResponse pelicula = await _Servicio.DatosPelicula(id);
            return Ok(pelicula);
        }

        [HttpPut("/api/v1/Pelicula/{id}")]
        public async Task<ActionResult> ActualizarPelicula(int id, PeliculaDTO pelicula)
        {
            string result = await _Servicio.LimitarCaracteres(pelicula); 

            if (result.Length > 0)
            {
                var respuesta = new { Motivo = "Ha excedido el límite de caracteres para " + result }; 
                return BadRequest(respuesta);
            }

            if (await _Servicio.ComprobarId(id)) 
            {
                var respuesta = new { Motivo = "El ID ingresado no se encuntra asociado a ninguna pelicula registrada en la base de datos, por favor ingrese uno válido" };
                return NotFound(respuesta);
            }

            if (await _Servicio.ConsultarNombre(id, pelicula)) 
            {
                var respuesta = new { Motivo = "Ya existe una pelicula con ese titulo, asegurese de escribir correctamente los datos de una pelicula que no se encuentre registrada en la base de datos" };
                return Conflict(respuesta);
            }

            PeliculaResponse peli = await _Servicio.ActulizarPelicula(id, pelicula); 

            if (peli == null)
            {
                var respuesta = new { Motivo = "No se han podido actualizar los datos, por favor ingrese correctamente el id de género" };
                return NotFound(respuesta);
            }

            return Ok(peli);
        }

    }
}
