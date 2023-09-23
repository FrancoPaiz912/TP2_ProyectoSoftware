using Aplicacion.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Infraestructura
{
    public interface IActualizarPeliculas
    {
        Task<bool> ActualizarPelicula(int id, PeliculaDTO pelicula);
    }
}
