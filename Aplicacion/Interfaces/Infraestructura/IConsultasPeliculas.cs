using Aplicacion.DTO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Infraestructura
{
    public interface IConsultasPeliculas
    {
        Task<bool> ComprobarNombre(int ID, PeliculaDTO nombre);
        Task<bool> ComprobarID(int id);
        Task<List<Funciones>> RecuperarPelicula(int id);
    }
}
