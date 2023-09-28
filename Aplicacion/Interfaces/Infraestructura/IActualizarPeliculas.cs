using Aplicacion.DTO;
using Dominio;

namespace Aplicacion.Interfaces.Infraestructura
{
    public interface IActualizarPeliculas
    {
        Task<Peliculas> ActualizarPelicula(int id, PeliculaDTO pelicula);
    }
}
