using Aplicacion.DTO;
using Aplicación.Interfaces.Infraestructura;
using Dominio;
using Infraestructura.EstructuraDB;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Querys
{
    public class Consulta_Funcion : IConsultas
    {
        private readonly Contexto_Cine _Contexto;

        public Consulta_Funcion(Contexto_Cine context)
        {
            _Contexto = context;
        }

        List<FuncionesDTO> IConsultas.ListarFunciones()
        {
            return (from Funciones in _Contexto.Funciones
                   join Peliculas in _Contexto.Peliculas on Funciones.PeliculaId equals Peliculas.Peliculasid
                   join Salas in _Contexto.Salas on Funciones.SalaId equals Salas.SalasId
                   join Genero in _Contexto.Generos on Peliculas.Genero equals Genero.GenerosId
                   select new FuncionesDTO
                   {
                       FuncionesId = Funciones.FuncionesId,
                       PeliculaId = Peliculas.Peliculasid,
                       GenerosId = Genero.GenerosId,
                       Titulo = Peliculas.Titulo,
                       Sinopsis = Peliculas.Sinopsis,
                       Poster = Peliculas.Poster,
                       Trailer = Peliculas.Trailer,
                       Sala = Salas.Nombre,
                       Capacidad = Salas.Capacidad,
                       Fecha = Funciones.Fecha,
                       Hora = Funciones.Tiempo,
                       genero = Genero.Nombre,
                   }).ToList();
        }

        List<FuncionesDTO> IConsultas.ListarFecha(DateTime? fecha, List<FuncionesDTO> result)
        {
            return (from Funciones in _Contexto.Funciones
                    join Peliculas in _Contexto.Peliculas on Funciones.PeliculaId equals Peliculas.Peliculasid
                    join Salas in _Contexto.Salas on Funciones.SalaId equals Salas.SalasId
                    join Genero in _Contexto.Generos on Peliculas.Genero equals Genero.GenerosId
                    where Funciones.Fecha == fecha
                    select new FuncionesDTO
                    {
                        FuncionesId = Funciones.FuncionesId,
                        PeliculaId = Peliculas.Peliculasid,
                        GenerosId = Genero.GenerosId,
                        Titulo = Peliculas.Titulo,
                        Sinopsis = Peliculas.Sinopsis,
                        Poster = Peliculas.Poster,
                        Trailer = Peliculas.Trailer,
                        Sala = Salas.Nombre,
                        Capacidad = Salas.Capacidad,
                        Fecha = Funciones.Fecha,
                        Hora = Funciones.Tiempo,
                        genero = Genero.Nombre,
                    }).ToList();
        }

        List<FuncionesDTO> IConsultas.ListarFunciones(int? id, List<FuncionesDTO> result)
        {
            return (from Funciones in _Contexto.Funciones
                    join Peliculas in _Contexto.Peliculas on Funciones.PeliculaId equals Peliculas.Peliculasid
                    join Salas in _Contexto.Salas on Funciones.SalaId equals Salas.SalasId
                    join Genero in _Contexto.Generos on Peliculas.Genero equals Genero.GenerosId
                    where Funciones.FuncionesId == id
                    select new FuncionesDTO
                    {
                        FuncionesId = Funciones.FuncionesId,
                        PeliculaId = Peliculas.Peliculasid,
                        GenerosId = Genero.GenerosId,
                        Titulo = Peliculas.Titulo,
                        Sinopsis = Peliculas.Sinopsis,
                        Poster = Peliculas.Poster,
                        Trailer = Peliculas.Trailer,
                        Sala = Salas.Nombre,
                        Capacidad = Salas.Capacidad,
                        Fecha = Funciones.Fecha,
                        Hora = Funciones.Tiempo,
                        genero = Genero.Nombre,
                    }).ToList();
        }

        List<FuncionesDTO> IConsultas.ListarPeliculas(int? id, List<FuncionesDTO> result)
        {
            return (from Funciones in _Contexto.Funciones
                    join Peliculas in _Contexto.Peliculas on Funciones.PeliculaId equals Peliculas.Peliculasid
                    join Salas in _Contexto.Salas on Funciones.SalaId equals Salas.SalasId
                    join Genero in _Contexto.Generos on Peliculas.Genero equals Genero.GenerosId
                    where Peliculas.Peliculasid == id
                    select new FuncionesDTO
                    {
                        FuncionesId = Funciones.FuncionesId,
                        PeliculaId = Peliculas.Peliculasid,
                        GenerosId = Genero.GenerosId,
                        Titulo = Peliculas.Titulo,
                        Sinopsis = Peliculas.Sinopsis,
                        Poster = Peliculas.Poster,
                        Trailer = Peliculas.Trailer,
                        Sala = Salas.Nombre,
                        Capacidad = Salas.Capacidad,
                        Fecha = Funciones.Fecha,
                        Hora = Funciones.Tiempo,
                        genero = Genero.Nombre,
                    }).ToList();
        }

        List<FuncionesDTO> IConsultas.ListarGeneros(int? id, List<FuncionesDTO> result)
        {
            //return _Contexto.Funciones.Include(s => s.Tickets)
            //    .Include(s => s.Salas)
            //    .Include(s => s.Peliculas)
            //    .ThenInclude(s => s.Generos).Where(s => s.SalaId == id).ToList();

            return (from Funciones in _Contexto.Funciones
                   join Peliculas in _Contexto.Peliculas on Funciones.PeliculaId equals Peliculas.Peliculasid
                   join Salas in _Contexto.Salas on Funciones.SalaId equals Salas.SalasId
                   join Genero in _Contexto.Generos on Peliculas.Genero equals Genero.GenerosId
                    where Genero.GenerosId == id
                    select new FuncionesDTO
                    {
                       FuncionesId=Funciones.FuncionesId,
                       PeliculaId=Peliculas.Peliculasid,
                       GenerosId=Genero.GenerosId,
                       Titulo = Peliculas.Titulo,
                       Sinopsis = Peliculas.Sinopsis,
                       Poster = Peliculas.Poster,
                       Trailer = Peliculas.Trailer,
                       Sala = Salas.Nombre,
                       Capacidad = Salas.Capacidad,
                       Fecha = Funciones.Fecha,
                       Hora = Funciones.Tiempo,
                       genero = Genero.Nombre,
                   }).ToList();

        }
    }
}
