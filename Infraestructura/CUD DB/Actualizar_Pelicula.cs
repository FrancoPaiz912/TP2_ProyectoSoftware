using Aplicacion.DTO;
using Aplicacion.Interfaces.Infraestructura;
using Dominio;
using Infraestructura.EstructuraDB;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Consultas_DB
{
    public class Actualizar_Pelicula : IActualizarPeliculas
    {
        private readonly Contexto_Cine _Contexto;

        public Actualizar_Pelicula(Contexto_Cine contexto)
        {
            _Contexto = contexto;
        }

        async Task<Peliculas> IActualizarPeliculas.ActualizarPelicula(int Id, PeliculaDTO peli)
        {
            Peliculas pelicula = await _Contexto.Peliculas.Include(s => s.Funciones).Include(s => s.Generos).FirstOrDefaultAsync(s => s.PeliculaId == Id);
            Generos genero = _Contexto.Generos.FirstOrDefault(s => s.GeneroId == peli.Genero);
            if (pelicula != null && genero != null)
            {
                pelicula.Titulo = peli.Titulo.ToUpper();
                pelicula.Sinopsis = peli.Sinopsis;
                pelicula.Poster = peli.Poster;
                pelicula.Trailer = peli.Trailer;
                pelicula.Genero = peli.Genero;
            }
            else
            {
                return null;
            }
            await _Contexto.SaveChangesAsync();
            return await _Contexto.Peliculas.Include(s => s.Funciones).Include(s => s.Generos).FirstOrDefaultAsync(s => s.PeliculaId == Id);
        }
    }
}
