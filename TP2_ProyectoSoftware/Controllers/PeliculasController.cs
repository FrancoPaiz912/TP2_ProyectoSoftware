using Aplicacion.DTO;
using Aplicacion.Interfaces.Aplicacion;
using Aplicacion.RespuestasHTTP;
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

        [HttpGet("/api/v1/Pelicula/{id}")]
        public async Task<ActionResult> GetInfoPelicula(int id)
        {
            if (id <= 0)
            {
                var respuesta = new { Motivo = "El ID ingresado no es valido, ingrese un id mayor a 0" };
                return BadRequest(respuesta);
            }

            if (await _Servicio.ComprobarId(id)) //Comprobamos que exista la pelicula 
            { //En caso de no exitir se arroja un mensaje HTTP404
                var respuesta = new { Motivo = "El ID ingresado no se encuentra asociado a ninguna pelicula registrada en la base de datos, por favor ingrese uno válido" };
                return NotFound(respuesta);
            }
            //Se realiza la query de la pelicula trayendo la respuesta a dar
            PeliculaResponse pelicula = await _Servicio.DatosPelicula(id);
            return Ok(pelicula);
        }

        [HttpPut("/api/v1/Pelicula/{id}")]
        public async Task<ActionResult> ActualizarPelicula(int id, PeliculaDTO pelicula)
        {
            string result = await _Servicio.LimitarCaracteres(pelicula); //Comprueba que no se exceda el límite establecido en la base de datos

            if (result.Length > 0)//Si la cadena excede los límites se arroja una respuesta HTTP404
            {
                var respuesta = new { Motivo = "Ha excedido el límite de caracteres para " + result }; //Multiples catch en vez de if?
                return BadRequest(respuesta);
            }

            if (await _Servicio.ComprobarId(id)) //Se comprueba que el ID de la pelicula exista y en caso de no hacerlo arroja el error HTTP404
                {
                var respuesta = new { Motivo = "El ID ingresado no se encuntra asociado a ninguna pelicula registrada en la base de datos, por favor ingrese uno válido"};
                return NotFound(respuesta);
                }

            if (await _Servicio.ConsultarNombre(id,pelicula)) //Se comprueba que otra pelicula registrada no tenga el mismo nombre
                {//Si existe una pelicula registrada con el mismo nombre se arroja el mensaje HTTP409 
                var respuesta = new { Motivo = "Ya existe una pelicula con ese titulo, asegurese de escribir correctamente los datos de una pelicula que no se encuentre registrada en la base de datos" };
                return Conflict(respuesta);
                }

            PeliculaResponse peli = await _Servicio.ActulizarPelicula(id, pelicula); //Se actualiza la pelicula

            if (peli == null)
                {//En caso de tener problemas en actualizar el genero se lanza el siguiente mensaje HTTP404
                var respuesta = new { Motivo = "No se han podido actualizar los datos, por favor ingrese correctamente el id de género"};
                return NotFound(respuesta);
                }
            
            return Ok(peli); 
        }

    }
}
