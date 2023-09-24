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

        async Task<List<Funciones>> IConsultasFunciones.ListarFunciones() //Retornar solo las tablas con las condiciones y armar el DTO en aplicacion
        {
            return await _Contexto.Funciones
                .Include(s => s.Tickets)
                .Include(s => s.Salas)
                .Include(s => s.Peliculas)
                .ThenInclude(s => s.Generos).ToListAsync();
        }

        async Task<List<Funciones>> IConsultasFunciones.ListarFecha(DateTime? fecha, List<CarteleraDTO> result)
        {
            return await _Contexto.Funciones.Include(s => s.Peliculas)
                .ThenInclude(s => s.Generos)
                .Include(s => s.Tickets)
                .Include(s => s.Salas).Where(S => S.Fecha ==fecha).ToListAsync();
        }

        async Task<List<Funciones>> IConsultasFunciones.ListarFunciones(int? id, List<CarteleraDTO> result)
        {
            return await _Contexto.Funciones.Include(s => s.Peliculas)
                .ThenInclude(s => s.Generos)
                .Include(s => s.Tickets)
                .Include(s => s.Salas).Where(S => S.FuncionesId == id).ToListAsync();
        }

        async Task<List<Funciones>> IConsultasFunciones.ListarPeliculas(int? id, List<CarteleraDTO> result)
        {
            return await _Contexto.Funciones.Include(s => s.Peliculas)
                .ThenInclude(s => s.Generos)
                .Include(s => s.Tickets)
                .Include(s => s.Salas).Where(S => S.PeliculaId == id).ToListAsync();
        }

        async Task<List<Funciones>> IConsultasFunciones.ListarGeneros(int? id, List<CarteleraDTO> result)
        {
            return await _Contexto.Funciones.Include(s => s.Salas)
                .Include(s => s.Tickets)
                .Include(s => s.Peliculas)
                .ThenInclude(s => s.Generos).Where(S => S.Peliculas.Generos.GenerosId == id).ToListAsync();

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
            Funciones func= await _Contexto.Funciones.Include(s => s.Peliculas).ThenInclude(s => s.Generos).Include(s => s.Salas).FirstOrDefaultAsync(s => s.FuncionesId == id);
            return func;
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
