using Aplicación.Interfaces.Infraestructura;
using Dominio;
using Infraestructura.EstructuraDB;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Querys
{
    public class Consulta_Funcion : IConsultasFunciones
    {
        private readonly Contexto_Cine _Contexto;

        public Consulta_Funcion(Contexto_Cine context)
        {
            _Contexto = context;
        }

        async Task<List<Funciones>> IConsultasFunciones.ListarFunciones(string? fecha, string? titulo, int? Genero)
        {
            return await _Contexto.Funciones
                .Include(s => s.Tickets)
                .Include(s => s.Salas)
                .Include(s => s.Peliculas)
                .ThenInclude(s => s.Generos).Where(s => (fecha != null ? (s.Fecha.Month == DateTime.Parse(fecha).Month && s.Fecha.Day == DateTime.Parse(fecha).Day) : true) && (titulo != null ? s.Peliculas.Titulo.Contains(titulo) : true) && (Genero != null ? s.Peliculas.Genero == Genero : true)).ToListAsync();
        }

        async Task<Funciones> IConsultasFunciones.RetornarRegistro()
        {
            return await _Contexto.Funciones.Include(s => s.Peliculas)
                .ThenInclude(s => s.Generos)
                .Include(s => s.Tickets)
                .Include(s => s.Salas).OrderByDescending(s => s.FuncionId).FirstOrDefaultAsync();
        }

        async Task<List<bool>> IConsultasFunciones.GetIDs(int IdPelicula, int IdSala)
        {
            List<bool> list = new List<bool>();

            if (_Contexto.Peliculas.Any(s => s.PeliculaId == IdPelicula)) list.Add(true);
            else list.Add(false);

            if (_Contexto.Salas.Any(s => s.SalaId == IdSala)) list.Add(true);
            else list.Add(false);

            return list;
        }

        async Task<Funciones> IConsultasFunciones.GetIdFuncion(int id)
        {
            return await _Contexto.Funciones.Include(s => s.Peliculas).ThenInclude(s => s.Generos).Include(s => s.Salas).FirstOrDefaultAsync(s => s.FuncionId == id);
        }

        async Task<bool> IConsultasFunciones.ComprobacionHoraria(int Salaid, DateTime Fecha, TimeSpan Horainicio)
        {
            TimeSpan HoraFinal = Horainicio + TimeSpan.FromHours(2) + TimeSpan.FromMinutes(30);
            List<Funciones> list = _Contexto.Funciones.Where(s => s.SalaId == Salaid && s.Fecha.Day == Fecha.Day && s.Fecha.Month == Fecha.Month).AsEnumerable()
                                                      .Where(s => s.Hora + TimeSpan.FromHours(2) + TimeSpan.FromMinutes(30) > Horainicio && Horainicio >= s.Hora
                                                      || s.Hora + TimeSpan.FromHours(2) + TimeSpan.FromMinutes(30) >= HoraFinal && HoraFinal > s.Hora).ToList();
            if (list.Count() == 0) return false;
            else return true;
        }

    }
}
