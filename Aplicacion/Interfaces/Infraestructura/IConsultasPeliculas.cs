using Aplicacion.DTO;
using Dominio;

namespace Aplicacion.Interfaces.Infraestructura
{
    public interface IConsultasPeliculas
    {
        Task<bool> ComprobarNombre(int ID, PeliculaDTO nombre);
        Task<bool> ComprobarID(int id);
        Task<Peliculas> RecuperarPelicula(int id);
    }
}
