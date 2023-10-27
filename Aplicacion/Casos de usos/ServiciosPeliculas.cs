using Aplicacion.DTO;
using Aplicacion.Interfaces.Aplicacion;
using Aplicacion.Interfaces.Infraestructura;
using Aplicacion.RespuestasHTTP;
using Dominio;

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
            return await _Consultas.ComprobarID(id); //Comprobamos que exista la pelicula 
        }

        async Task<bool> IServiciosPeliculas.ConsultarNombre(int ID, PeliculaDTO nombre)
        {
            return await _Consultas.ComprobarNombre(ID, nombre); //Comprobamos el nombre de la pelicula y retornamos un booleano acorde si existe o no
        }

        async Task<PeliculaResponse> IServiciosPeliculas.ActulizarPelicula(int Id, PeliculaDTO peli)
        {
            List<InfoFuncionesParaPeliculaRespuesta> list = new List<InfoFuncionesParaPeliculaRespuesta>();
            Peliculas pelicula = await _Actualizar.ActualizarPelicula(Id, peli); //Se envian los datos para actualizar la pelicula en la base de datos
            if (pelicula != null) { 
                foreach (Funciones func in pelicula.Funciones)
                {
                    list.Add(new InfoFuncionesParaPeliculaRespuesta
                    {
                        FuncionId = func.FuncionId,
                        Fecha = func.Fecha,
                        Horario = func.Hora,
                    });
                }

                return new PeliculaResponse //Creamos el response de pelicula
                {
                    Peliculaid = pelicula.PeliculaId,
                    Titulo = pelicula.Titulo,
                    Sinopsis = pelicula.Sinopsis,
                    Poster = pelicula.Poster,
                    Trailer = pelicula.Trailer,
                    Genero = new GeneroRespuesta
                    {
                        Id = pelicula.Generos.GeneroId,
                        Nombre = pelicula.Generos.Nombre,
                    },
                    funciones = list, 
                };
            }
            else return null;
        }

        async Task<string> IServiciosPeliculas.LimitarCaracteres(PeliculaDTO pelicula) //Se compruba que no se excedan las limitaciones de caracteres. En caso de no hacerlo devuelve una cadena vacía
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

        async Task<PeliculaResponse> IServiciosPeliculas.DatosPelicula(int id)
        {
            List<InfoFuncionesParaPeliculaRespuesta> respuesta = new List<InfoFuncionesParaPeliculaRespuesta>();
            Peliculas pelicula = await _Consultas.RecuperarPelicula(id); //Se busca en la base de datos la información de la pelicula incluyendo sus multiples funciones
            foreach (Funciones func in pelicula.Funciones) //Se agrega a una lista las funciones relacionadas a la pelicula.
            {
                respuesta.Add(new InfoFuncionesParaPeliculaRespuesta
                {
                    FuncionId = func.FuncionId,
                    Fecha = func.Fecha,
                    Horario = func.Hora,
                });
            }

            return new PeliculaResponse //Se fabrica el response de pelicula incluyendo la cantidad de funciones que tiene 
                {
                    Peliculaid = pelicula.PeliculaId, //Al tener en la lista todas las funciones la misma informacion de pelicula se utiliza la de la posición 0 solamente para asegurarse que exista, pero podría ser cualquier otra que exista.
                    Titulo = pelicula.Titulo,
                    Sinopsis = pelicula.Sinopsis,
                    Poster = pelicula.Poster,
                    Trailer = pelicula.Trailer,
                    Genero = new GeneroRespuesta
                    {
                        Id = pelicula.Generos.GeneroId,
                        Nombre = pelicula.Generos.Nombre,
                    },
                    funciones = respuesta,
                };
        }
    }
}
