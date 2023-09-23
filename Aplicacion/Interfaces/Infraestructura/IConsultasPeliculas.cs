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
        Task<bool> ComprobarNombre(string nombre);
        Task<bool> ComprobarID(int id);
        Task<Peliculas> RecuperarPelicula(int id);
    }
}
