using Aplicacion.DTO;
using Aplicacion.RespuestasHTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Aplicacion
{
    public interface IServiciosPeliculas
    {
        Task<bool> ConsultarNombre(int ID, PeliculaDTO nombre);
        Task<bool> ComprobarId(int id);
        Task<PeliculaResponse> ActulizarPelicula(int Id, PeliculaDTO peli);
        Task<string> LimitarCaracteres(PeliculaDTO pelicula);
        Task<PeliculaResponse> DatosPelicula(int id);
    }
}
