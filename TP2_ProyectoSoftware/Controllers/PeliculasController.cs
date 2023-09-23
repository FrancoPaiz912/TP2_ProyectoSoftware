using Aplicacion.DTO;
using Aplicacion.Interfaces.Aplicacion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("{ID}")]
        public async Task<ActionResult> GetInfoPelicula(int ID)
        {
            if (await _Servicio.ComprobarId(ID))
            {
                return BadRequest("El ID ingresado no se encuntra asociado a ninguna pelicula registrada en la base de datos, por favor ingrese uno válido");
            }

            return Ok(await _Servicio.DatosPelicula(ID));
        }

        [HttpPut("{ID}")] //Si se ponen acá se hacen obligatorios
        public async Task<ActionResult> ActualizarPelicula(int ID, PeliculaDTO pelicula)
        {
            string result = await _Servicio.LimitarCaracteres(pelicula);

            if (result.Length > 0)
            {
                return BadRequest("Ha excedido el límite de caracteres para " + result);
            }

            if (await _Servicio.ComprobarId(ID))
                {
                    return BadRequest("El ID ingresado no se encuntra asociado a ninguna pelicula registrada en la base de datos, por favor ingrese uno válido");
                }

            if (await _Servicio.ConsultarNombre(pelicula.Titulo))
                {
                    return BadRequest("Ya existe una pelicula con ese titulo, asegurese de escribir correctamente los datos de una pelicula que no se encuentre registrada en la base de datos");
                }

            if (!await _Servicio.ActulizarPelicula(ID, pelicula))
                {
                    return BadRequest("No se han podido actualizar los datos, compruebe que el genero exista en la base de datos");
                }
            
            return Ok("Los datos se han actualizado correctamente");
        }

    }
}
