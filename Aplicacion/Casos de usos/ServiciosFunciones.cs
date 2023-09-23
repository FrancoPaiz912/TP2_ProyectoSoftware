using Aplicacion.DTO;
using Aplicacion.Excepciones;
using Aplicacion.Interfaces.Aplicacion;
using Aplicación.Interfaces.Infraestructura;
using Aplicacion.Interfaces.Infraestructura;
using Aplicacion.RespuestasHTTP;
using Dominio;
using System.Security.Cryptography;

namespace Aplicacion.Casos_de_usos
{
    public class ServiciosFunciones : IServiciosFunciones
    {
        private readonly IConsultasFunciones _Consultas;
        private readonly IAgregar _Agregar;
        private readonly IEliminar _Eliminar;
        public ServiciosFunciones(IConsultasFunciones consultas, IAgregar Agregar, IEliminar Eliminar)
        {
            _Consultas = consultas;
            _Agregar = Agregar;
            _Eliminar = Eliminar;
        }

        //Gets
        async Task<List<CarteleraDTO>> IServiciosFunciones.GetFuncionesDTO()
        {
            List<Funciones> Funciones = await _Consultas.ListarFunciones();
            List<CarteleraDTO> result= new List<CarteleraDTO> ();
            foreach (var item in Funciones)
            {
                result.Add(new CarteleraDTO
                {
                    FuncionesId = item.FuncionesId,
                    PeliculaId = item.Peliculas.Peliculasid,
                    GenerosId = item.Peliculas.Generos.GenerosId,
                    Titulo = item.Peliculas.Titulo,
                    Sinopsis = item.Peliculas.Sinopsis,
                    Poster = item.Peliculas.Poster,
                    Trailer = item.Peliculas.Trailer,
                    Sala = item.Salas.Nombre,
                    Capacidad = item.Salas.Capacidad,
                    Fecha = item.Fecha,
                    Hora = item.Hora,
                    genero = item.Peliculas.Generos.Nombre,
                });
            }
            return result;
        }

        async Task<List<CarteleraDTO>> IServiciosFunciones.GetFuncionesDia(DateTime? dia, List<CarteleraDTO> funciones)
        {
            List<Funciones> result = new List<Funciones>();
            if (result.Count() == 0 && dia != null)
            {
                result = await _Consultas.ListarFecha(dia, funciones);
                foreach (var item in result)
                {
                    funciones.Add(new CarteleraDTO
                    {
                        FuncionesId = item.FuncionesId,
                        PeliculaId = item.Peliculas.Peliculasid,
                        GenerosId = item.Peliculas.Generos.GenerosId,
                        Titulo = item.Peliculas.Titulo,
                        Sinopsis = item.Peliculas.Sinopsis,
                        Poster = item.Peliculas.Poster,
                        Trailer = item.Peliculas.Trailer,
                        Sala = item.Salas.Nombre,
                        Capacidad = item.Salas.Capacidad,
                        Fecha = item.Fecha,
                        Hora = item.Hora,
                        genero = item.Peliculas.Generos.Nombre,
                    });
                }
                return funciones;
            }
            else
            {
                funciones = funciones.Where(s => s.Fecha == dia).ToList();  
                return funciones;
            }
        }

        async Task<List<CarteleraDTO>> IServiciosFunciones.GetFuncionesNombrePelicula(int? PeliculaID, List<CarteleraDTO> funciones)
        {
            List<Funciones> result = new List<Funciones>();
            if (funciones.Count() == 0 && PeliculaID != null)
            {
                result = await _Consultas.ListarPeliculas(PeliculaID, funciones);
                foreach (var item in result)
                {
                    funciones.Add(new CarteleraDTO
                    {
                        FuncionesId = item.FuncionesId,
                        PeliculaId = item.Peliculas.Peliculasid,
                        GenerosId = item.Peliculas.Generos.GenerosId,
                        Titulo = item.Peliculas.Titulo,
                        Sinopsis = item.Peliculas.Sinopsis,
                        Poster = item.Peliculas.Poster,
                        Trailer = item.Peliculas.Trailer,
                        Sala = item.Salas.Nombre,
                        Capacidad = item.Salas.Capacidad,
                        Fecha = item.Fecha,
                        Hora = item.Hora,
                        genero = item.Peliculas.Generos.Nombre,
                    });
                }
                return funciones;
            }
            else
            {
                funciones = funciones.Where(s => s.PeliculaId == PeliculaID).ToList();  
                return funciones;
            }
        }

        async Task<List<CarteleraDTO>> IServiciosFunciones.GetFuncionesGenero(int? GeneroID, List<CarteleraDTO> funciones)
        {
            List<Funciones> result = new List<Funciones>();
            if (funciones.Count() == 0 && GeneroID != null)
            {
                result = await _Consultas.ListarGeneros(GeneroID, funciones);
                foreach (var item in result)
                {
                    funciones.Add(new CarteleraDTO
                    {
                        FuncionesId = item.FuncionesId,
                        PeliculaId = item.Peliculas.Peliculasid,
                        GenerosId = item.Peliculas.Generos.GenerosId,
                        Titulo = item.Peliculas.Titulo,
                        Sinopsis = item.Peliculas.Sinopsis,
                        Poster = item.Peliculas.Poster,
                        Trailer = item.Peliculas.Trailer,
                        Sala = item.Salas.Nombre,
                        Capacidad = item.Salas.Capacidad,
                        Fecha = item.Fecha,
                        Hora = item.Hora,
                        genero = item.Peliculas.Generos.Nombre,
                    });
                }
                return funciones;
            }
            else
            {
                funciones = funciones.Where(s => s.GenerosId == GeneroID).ToList();
                return funciones;
            }
        }

        async Task IServiciosFunciones.AddFunciones(Funciones funcion)
        {
            _Agregar.AgregarFuncion(funcion);
        }

        async Task<List<bool>> IServiciosFunciones.GetId(int IdPelicula,int IdSala)
        {
            return await _Consultas.GetIDs(IdPelicula,IdSala);
        }

        async Task<Funciones> IServiciosFunciones.ComprobarFunciones(int id)
        {
            return await _Consultas.GetIdFuncion(id);
        }

        async Task<bool> IServiciosFunciones.EliminarFuncion(Funciones funcion)
        {
            return await _Eliminar.RemoverFuncion(funcion);
        }

        async Task<bool> IServiciosFunciones.ComprobarHorario(int Salaid, DateTime Fecha, TimeSpan Hora)
        {
            return await _Consultas.ComprobacionHoraria(Salaid, Fecha, Hora);
        }

        async Task<TicketRespuesta> IServiciosFunciones.GenerarTicket(TicketDTO ticket)
        {
            //Crear ticket y devolver datos de respuesta.
            Funciones Response = await _Agregar.AgregarTicket(new Tickets
            {
                FuncionId = ticket.FuncionId,
                Usuario = ticket.Usuario,
            });

            return new TicketRespuesta
            {
                Usuario = ticket.Usuario,
                Titulo = Response.Peliculas.Titulo,
                Sinopsis = Response.Peliculas.Sinopsis,
                Fecha = Response.Fecha,
                Hora = Response.Hora,
                Sala = Response.Salas.Nombre,
                genero = Response.Peliculas.Generos.Nombre,
            };
        }

        async Task<FuncionRespuesta> IServiciosFunciones.GetDatosFuncion(int id)
        {
            Funciones funcion = await _Consultas.GetIdFuncion(id);
            if (funcion != null)
            {
                return new FuncionRespuesta
                {
                    Titulo = funcion.Peliculas.Titulo,
                    Sinopsis = funcion.Peliculas.Sinopsis,
                    Poster = funcion.Peliculas.Poster,
                    Trailer = funcion.Peliculas.Trailer,
                    Sala = funcion.Salas.Nombre,
                    Capacidad = funcion.Salas.Capacidad, //Podrías devolver disponibilidad
                    Fecha = funcion.Fecha.Date,
                    Hora = funcion.Hora,
                    genero = funcion.Peliculas.Generos.Nombre,
                };
            }
            return null; 
        }
    }
}
