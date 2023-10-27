using Aplicacion.DTO;
using Aplicacion.Interfaces.Infraestructura;
using Dominio;
using Infraestructura.EstructuraDB;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Consultas_DB
{
    public class Consultas_Peliculas : IConsultasPeliculas
    {
        private readonly Contexto_Cine _Contexto;

        public Consultas_Peliculas(Contexto_Cine contexto)
        {
            _Contexto = contexto;
        }

        async Task<bool> IConsultasPeliculas.ComprobarNombre(int ID, PeliculaDTO peli)
        {
            if (await _Contexto.Peliculas.FirstOrDefaultAsync(s => s.Titulo == peli.Titulo && s.PeliculaId != ID) == null)
            {
                return false;
            }
            else return true;
        }

        async Task<bool> IConsultasPeliculas.ComprobarID(int id)
        {
            if (await _Contexto.Peliculas.FirstOrDefaultAsync(s => s.PeliculaId == id) == null)
            {
                return true;
            }
            else return false;
        }

        async Task<Peliculas> IConsultasPeliculas.RecuperarPelicula(int id)
        {
            return await _Contexto.Peliculas.Include(s => s.Funciones).Include(s => s.Generos).FirstOrDefaultAsync(s => s.PeliculaId == id);
        }
    }
}
