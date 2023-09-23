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

        async Task<bool> IActualizarPeliculas.ActualizarPelicula(int Id, PeliculaDTO peli)
        {
            Peliculas pelicula = await _Contexto.Peliculas.FirstOrDefaultAsync(s => s.Peliculasid == Id);
            int genero =  _Contexto.Generos.Where(s => s.Nombre == peli.Genero).Select(s=> s.GenerosId).FirstOrDefault();
            if (pelicula != null && genero!= 0) 
            {
                pelicula.Titulo=peli.Titulo; //Comprobar la cantidad de caracteres ingresados(que no excedan el límite)
                pelicula.Sinopsis=peli.Sinopsis;
                pelicula.Poster=peli.Poster;
                pelicula.Trailer=peli.Trailer;
                pelicula.Genero = genero; //Controlar si el género no existe
            } else if (genero == 0)
            {
                return false;
            }
            await _Contexto.SaveChangesAsync();
            return true;
        }
    }
}
