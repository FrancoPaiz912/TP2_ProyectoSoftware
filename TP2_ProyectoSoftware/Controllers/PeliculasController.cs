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

        [HttpGet("{ID}")]
        public async Task<ActionResult> GetInfoPelicula(int ID)
        {
            if (await _Servicio.ComprobarId(ID)) //Comprobamos que exista la pelicula 
            { //En caso de no exitir se arroja un mensaje HTTP404
                var respuesta = new { Motivo = "El ID ingresado no se encuentra asociado a ninguna pelicula registrada en la base de datos, por favor ingrese uno válido" };
                return NotFound(respuesta);
            }
            //Se realiza la query de la pelicula trayendo la respuesta a dar
            PeliculaCompletaResponse pelicula = await _Servicio.DatosPelicula(ID);
            return Ok(pelicula);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult> ActualizarPelicula(int ID, PeliculaDTO pelicula)
        {
            string result = await _Servicio.LimitarCaracteres(pelicula); //Comprueba que no se exceda el límite establecido en la base de datos

            if (result.Length > 0)//Si la cadena excede los límites se arroja una respuesta HTTP404
            {
                var respuesta = new { Motivo = "Ha excedido el límite de caracteres para " + result };
                return BadRequest(respuesta);
            }

            if (await _Servicio.ComprobarId(ID)) //Se comprueba que el ID de la pelicula exista y en caso de no hacerlo arroja el error HTTP404
                {
                var respuesta = new { Motivo = "El ID ingresado no se encuntra asociado a ninguna pelicula registrada en la base de datos, por favor ingrese uno válido"};
                return NotFound(respuesta);
                }

            if (await _Servicio.ConsultarNombre(ID,pelicula)) //Se comprueba que otra pelicula registrada no tenga el mismo nombre
                {//Si existe una pelicula registrada con el mismo nombre se arroja el mensaje HTTP409 
                var respuesta = new { Motivo = "Ya existe una pelicula con ese titulo, asegurese de escribir correctamente los datos de una pelicula que no se encuentre registrada en la base de datos" };
                return Conflict(respuesta);
                }

            PeliculaCompletaResponse peli = await _Servicio.ActulizarPelicula(ID, pelicula); //Se actualiza la pelicula

            if (peli == null)
                {//En caso de tener problemas en actualizar el genero se lanza el siguiente mensaje HTTP404
                var respuesta = new { Motivo = "No se han podido actualizar los datos, por favor ingrese correctamente el id de género"};
                return NotFound(respuesta);
                }
            
            return Ok(pelicula); 
        }

    }
}
