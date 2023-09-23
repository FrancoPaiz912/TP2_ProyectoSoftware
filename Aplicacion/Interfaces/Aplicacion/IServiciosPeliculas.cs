using Aplicacion.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Aplicacion
{
    public interface IServiciosPeliculas
    {
        Task<bool> ConsultarNombre(string nombre);
        Task<bool> ComprobarId(int id);
        Task<bool> ActulizarPelicula(int Id, PeliculaDTO peli);
        Task<string> LimitarCaracteres(PeliculaDTO pelicula);
        Task<PeliculaDTO> DatosPelicula(int id);
    }
}
