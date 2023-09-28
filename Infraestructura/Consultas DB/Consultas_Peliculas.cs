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
        {//Devuelve booleano acorde si existe o no la pelicula
            if (await _Contexto.Peliculas.FirstOrDefaultAsync(s => s.Titulo == peli.Titulo && s.Peliculasid != ID) == null)
            {
                return false;
            }
            else return true;
        }

        async Task<bool> IConsultasPeliculas.ComprobarID(int id)
        {//Comprobamos que exista la pelicula 
            if (await _Contexto.Peliculas.FirstOrDefaultAsync(s => s.Peliculasid == id) == null)
            {
                return true;
            }
            else return false;
        }

        async Task<Peliculas> IConsultasPeliculas.RecuperarPelicula(int id)
        {//Se buscan las funciones asociadas a una pelicula así como también el genero de la misma.
            return await _Contexto.Peliculas.Include(s => s.Funciones).Include(s => s.Generos).FirstOrDefaultAsync(s => s.Peliculasid == id);
        }
    }
}
