using Aplicacion.RespuestasHTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Aplicacion
{
    public interface IServiciosSalas
    {
        Task<AsientosResponse> CapacidadDisponible(int idFuncion);
    }
}
