using Aplicacion.DTO;
using Aplicacion.Interfaces.Infraestructura;
using Dominio;
using Infraestructura.EstructuraDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Peliculas pelicula = await _Contexto.Peliculas.Include(s=> s.Funciones).Include(s => s.Generos).FirstOrDefaultAsync(s => s.Peliculasid == Id);
            //Peliculas funciones = pelicula.FirstOrDefault();
            Generos genero =  _Contexto.Generos.FirstOrDefault(s=> s.GenerosId == peli.Genero);
            if (pelicula != null && genero != null) 
            {
                pelicula.Titulo=peli.Titulo;
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
