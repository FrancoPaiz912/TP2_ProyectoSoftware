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

        async Task<List<Funciones>> IConsultasFunciones.ListarFunciones(string? fecha, string titulo, int? Genero)
        {
            return await _Contexto.Funciones //Realizo las consultas acorde los parametros utilizando un ternario condicional en cada caso para en caso de que el parametro sea null traer todos los datos
                .Include(s => s.Tickets)
                .Include(s => s.Salas)
                .Include(s => s.Peliculas)
                .ThenInclude(s => s.Generos).Where( s => (fecha != null ? (s.Fecha.Month == DateTime.Parse(fecha).Month && s.Fecha.Day == DateTime.Parse(fecha).Day) : true) && (titulo!=null ? s.Peliculas.Titulo.Contains(titulo) : true) && (Genero != null ? s.Peliculas.Genero==Genero : true)).ToListAsync();
        }

        async Task<Funciones> IConsultasFunciones.RetornarRegistro()
        {
            return await _Contexto.Funciones.Include(s => s.Peliculas)
                .ThenInclude(s => s.Generos)
                .Include(s => s.Tickets)
                .Include(s => s.Salas).OrderByDescending(s => s.FuncionesId).FirstOrDefaultAsync();
        }

        async Task<List<bool>> IConsultasFunciones.GetIDs(int IdPelicula, int IdSala)
        {
            List<bool> list= new List<bool>(); //Compruebo si las ID existen y agrego un boolean según corresponda

            if (_Contexto.Peliculas.Any(s => s.Peliculasid == IdPelicula)) list.Add(true);
            else list.Add(false);

            if (_Contexto.Salas.Any(s => s.SalasId == IdSala)) list.Add(true);
            else list.Add(false);

            return list;
        }

        async Task<Funciones> IConsultasFunciones.GetIdFuncion(int id)
        {//Devolvemos la función si existe
            return await _Contexto.Funciones.Include(s => s.Peliculas).ThenInclude(s => s.Generos).Include(s => s.Salas).FirstOrDefaultAsync(s => s.FuncionesId == id);
        }

        async Task<bool> IConsultasFunciones.ComprobacionHoraria(int Salaid, DateTime Fecha, TimeSpan Horainicio)
        { //Compruebo que se respete la franja horaria de 2:30 hs entre peliculas. En caso de no respetarse, agrega las funciones que impiden que esto se produzca a una lista
            TimeSpan HoraFinal = Horainicio + TimeSpan.FromHours(2) + TimeSpan.FromMinutes(30);
            List<Funciones> list =  _Contexto.Funciones.Where(s => s.SalaId == Salaid && s.Fecha.Day == Fecha.Day && s.Fecha.Month == Fecha.Month).AsEnumerable()
                                                      .Where(s => s.Hora + TimeSpan.FromHours(2) + TimeSpan.FromMinutes(30) > Horainicio && Horainicio >= s.Hora
                                                      || s.Hora + TimeSpan.FromHours(2) + TimeSpan.FromMinutes(30) >= HoraFinal && HoraFinal > s.Hora).ToList(); 
            if (list.Count() == 0) return false; //Si existe una función que se superponga con el horario que se desea ingresar en la misma sala se devuelve false
            else return true; //En caso contrario se devuelve un true
        }

    }
}
