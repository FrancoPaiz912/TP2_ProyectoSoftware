using Aplicacion.DTO;
using Aplicacion.Interfaces.Aplicacion;
using Aplicacion.Interfaces.Infraestructura;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Casos_de_usos
{
    public class ServiciosPeliculas : IServiciosPeliculas
    {
        private readonly IActualizarPeliculas _Actualizar;
        private readonly IConsultasPeliculas _Consultas;

        public ServiciosPeliculas(IActualizarPeliculas Actualizar, IConsultasPeliculas consultas)
        {
            _Actualizar = Actualizar;
            _Consultas = consultas;
        }

        async Task<bool> IServiciosPeliculas.ComprobarId(int id)
        {
            return await _Consultas.ComprobarID(id);
        }

        async Task<bool> IServiciosPeliculas.ConsultarNombre(string nombre)
        {
            return await _Consultas.ComprobarNombre(nombre);
        }

        async Task<bool> IServiciosPeliculas.ActulizarPelicula(int Id, PeliculaDTO peli)
        {
            return await _Actualizar.ActualizarPelicula(Id,peli);
        }

        async Task<string> IServiciosPeliculas.LimitarCaracteres(PeliculaDTO pelicula)
        {
            if (pelicula.Titulo.Length > 50)
            {
                return "el titulo, por favor no ingrese más de 50 carácteres";
            }
            if (pelicula.Sinopsis.Length > 255)
            {
                return "la sinopsis, por favor no ingrese más de 255 carácteres";
            }
            if (pelicula.Poster.Length > 100)
            {
                return "el poster, por favor no ingrese más de 100 carácteres";
            }
            if (pelicula.Trailer.Length > 100)
            {
                return "el trailer, por favor no ingrese más de 100 carácteres";
            }

            return "";
        }

        async Task<PeliculaDTO> IServiciosPeliculas.DatosPelicula(int id)
        {
            Peliculas pelicula = await _Consultas.RecuperarPelicula(id);
            return new PeliculaDTO
            {
                Titulo = pelicula.Titulo,
                Sinopsis = pelicula.Sinopsis,
                Poster = pelicula.Poster,
                Trailer = pelicula.Trailer,
                Genero = pelicula.Generos.Nombre
            };
        }
    }
}
