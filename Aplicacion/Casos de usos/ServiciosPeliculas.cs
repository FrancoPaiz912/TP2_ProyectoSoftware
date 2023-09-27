using Aplicacion.DTO;
using Aplicacion.Interfaces.Aplicacion;
using Aplicacion.Interfaces.Infraestructura;
using Aplicacion.RespuestasHTTP;
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

        async Task<bool> IServiciosPeliculas.ConsultarNombre(int ID, PeliculaDTO nombre)
        {
            return await _Consultas.ComprobarNombre(ID, nombre);
        }

        async Task<PeliculaCompletaResponse> IServiciosPeliculas.ActulizarPelicula(int Id, PeliculaDTO peli)
        {
            List<FuncionRespuesta> list = new List<FuncionRespuesta>();
            Peliculas pelicula = await _Actualizar.ActualizarPelicula(Id, peli);
            if (pelicula != null) {
                foreach (var item in pelicula.Funciones)
                {
                    list.Add(new FuncionRespuesta
                    {
                        FuncionId = item.FuncionesId,
                        Fecha = item.Fecha,
                        Horario = item.Hora,
                    });
                }
                return new PeliculaCompletaResponse
                {
                    Peliculaid = pelicula.Peliculasid,
                    Titulo = pelicula.Titulo,
                    Sinopsis = pelicula.Sinopsis,
                    Poster = pelicula.Poster,
                    Trailer = pelicula.Trailer,
                    Genero = new GeneroRespuesta
                    {
                        Id = pelicula.Generos.GenerosId,
                        Nombre = pelicula.Generos.Nombre,
                    },
                    funciones = list, //Crear funciones respuestas
                };
            }
            else return null;
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

        async Task<PeliculaCompletaResponse> IServiciosPeliculas.DatosPelicula(int id)
        {
            List<FuncionRespuesta> respuesta = new List<FuncionRespuesta>();
            List<Funciones> pelicula = await _Consultas.RecuperarPelicula(id);
            if (pelicula.Count > 0)
            {
                foreach (Funciones func in pelicula)
                {
                    respuesta.Add(new FuncionRespuesta
                    {
                        FuncionId = func.FuncionesId,
                        Fecha = func.Fecha,
                        Horario = func.Hora,
                    });
                }

                return new PeliculaCompletaResponse
                {
                    Peliculaid = pelicula[0].PeliculaId,
                    Titulo = pelicula[0].Peliculas.Titulo,
                    Sinopsis = pelicula[0].Peliculas.Sinopsis,
                    Poster = pelicula[0].Peliculas.Poster,
                    Trailer = pelicula[0].Peliculas.Trailer,
                    Genero = new GeneroRespuesta
                    {
                        Id = pelicula[0].Peliculas.Generos.GenerosId,
                        Nombre = pelicula[0].Peliculas.Generos.Nombre,
                    },
                    funciones = respuesta,
                };
            }
            else return null;
        }
    }
}
