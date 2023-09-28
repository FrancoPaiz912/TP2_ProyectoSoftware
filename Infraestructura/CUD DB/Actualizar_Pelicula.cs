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
        {//Se busca la pelicula y el genero. Si no se encuentran se devuelve un null dado que no se pudo actualizar la pelicula (al comprobar que la pelicula existe con anterioridad esto quiere decir que es un problema del id de genero)
            Peliculas pelicula = await _Contexto.Peliculas.Include(s=> s.Funciones).Include(s => s.Generos).FirstOrDefaultAsync(s => s.Peliculasid == Id);
            Generos genero =  _Contexto.Generos.FirstOrDefault(s=> s.GenerosId == peli.Genero);
            if (pelicula != null && genero != null) 
            {
                pelicula.Titulo=peli.Titulo.ToUpper();
                pelicula.Sinopsis=peli.Sinopsis;
                pelicula.Poster=peli.Poster;
                pelicula.Trailer=peli.Trailer;
                pelicula.Genero = peli.Genero; 
            } else
            {
                return null;
            }
            await _Contexto.SaveChangesAsync(); 
            return pelicula;
        }
    }
}
