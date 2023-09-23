using Aplicacion.DTO;
using Aplicación.Interfaces.Infraestructura;
using Dominio;
using Infraestructura.EstructuraDB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infraestructura.Querys
{
    public class Consulta_Funcion : IConsultasFunciones
    {
        private readonly Contexto_Cine _Contexto;

        public Consulta_Funcion(Contexto_Cine context)
        {
            _Contexto = context;
        }

        async Task<List<CarteleraDTO>> IConsultasFunciones.ListarFunciones() //Retornar solo las tablas con las condiciones y armar el DTO en aplicacion
        {
            return (from Funciones in _Contexto.Funciones
                   join Peliculas in _Contexto.Peliculas on Funciones.PeliculaId equals Peliculas.Peliculasid
                   join Salas in _Contexto.Salas on Funciones.SalaId equals Salas.SalasId
                   join Genero in _Contexto.Generos on Peliculas.Genero equals Genero.GenerosId
                   select new CarteleraDTO
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
                       Hora = Funciones.Hora,
                       genero = Genero.Nombre,
                   }).ToList();
        }

        async Task<List<CarteleraDTO>> IConsultasFunciones.ListarFecha(DateTime? fecha, List<CarteleraDTO> result)
        {
            return (from Funciones in _Contexto.Funciones
                    join Peliculas in _Contexto.Peliculas on Funciones.PeliculaId equals Peliculas.Peliculasid
                    join Salas in _Contexto.Salas on Funciones.SalaId equals Salas.SalasId
                    join Genero in _Contexto.Generos on Peliculas.Genero equals Genero.GenerosId
                    where Funciones.Fecha == fecha
                    select new CarteleraDTO
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
                        Hora = Funciones.Hora,
                        genero = Genero.Nombre,
                    }).ToList();
        }

        async Task<List<CarteleraDTO>> IConsultasFunciones.ListarFunciones(int? id, List<CarteleraDTO> result)
        {
            return (from Funciones in _Contexto.Funciones
                    join Peliculas in _Contexto.Peliculas on Funciones.PeliculaId equals Peliculas.Peliculasid
                    join Salas in _Contexto.Salas on Funciones.SalaId equals Salas.SalasId
                    join Genero in _Contexto.Generos on Peliculas.Genero equals Genero.GenerosId
                    where Funciones.FuncionesId == id
                    select new CarteleraDTO
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
                        Hora = Funciones.Hora,
                        genero = Genero.Nombre,
                    }).ToList();
        }

        async Task<List<CarteleraDTO>> IConsultasFunciones.ListarPeliculas(int? id, List<CarteleraDTO> result)
        {
            return (from Funciones in _Contexto.Funciones
                    join Peliculas in _Contexto.Peliculas on Funciones.PeliculaId equals Peliculas.Peliculasid
                    join Salas in _Contexto.Salas on Funciones.SalaId equals Salas.SalasId
                    join Genero in _Contexto.Generos on Peliculas.Genero equals Genero.GenerosId
                    where Peliculas.Peliculasid == id
                    select new CarteleraDTO
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
                        Hora = Funciones.Hora,
                        genero = Genero.Nombre,
                    }).ToList();
        }

        async Task<List<CarteleraDTO>> IConsultasFunciones.ListarGeneros(int? id, List<CarteleraDTO> result)
        {
            return (from Funciones in _Contexto.Funciones
                   join Peliculas in _Contexto.Peliculas on Funciones.PeliculaId equals Peliculas.Peliculasid
                   join Salas in _Contexto.Salas on Funciones.SalaId equals Salas.SalasId
                   join Genero in _Contexto.Generos on Peliculas.Genero equals Genero.GenerosId
                    where Genero.GenerosId == id
                    select new CarteleraDTO
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
                       Hora = Funciones.Hora,
                       genero = Genero.Nombre,
                   }).ToList();

        }

        async Task<List<bool>> IConsultasFunciones.GetIDs(int IdPelicula, int IdSala)
        {
            List<bool> list= new List<bool>();

            if (_Contexto.Peliculas.Any(s => s.Peliculasid == IdPelicula)) list.Add(true);
            else list.Add(false);

            if (_Contexto.Salas.Any(s => s.SalasId == IdSala)) list.Add(true);
            else list.Add(false);

            return list;
        }

        async Task<Funciones> IConsultasFunciones.GetIdFuncion(int id)
        {
            return await _Contexto.Funciones.FirstOrDefaultAsync(s => s.FuncionesId == id);
        }

        async Task<bool> IConsultasFunciones.ComprobacionHoraria(int Salaid, DateTime Fecha, TimeSpan Horainicio)
        {
            TimeSpan HoraFinal = Horainicio + TimeSpan.FromHours(2) + TimeSpan.FromMinutes(30);
            List<Funciones> list = _Contexto.Funciones.Where(s => s.SalaId == Salaid && s.Fecha == Fecha).AsEnumerable()
                .Where(s => s.Hora + TimeSpan.FromHours(2) + TimeSpan.FromMinutes(30) > Horainicio && Horainicio >= s.Hora
                 || s.Hora + TimeSpan.FromHours(2) + TimeSpan.FromMinutes(30) >= HoraFinal && HoraFinal > s.Hora).ToList(); 
            if (list.Count() == 0) return false;
            else return true; 
        }

        
    }
}
