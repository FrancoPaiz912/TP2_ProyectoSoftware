using Aplicacion.RespuestasHTTP;

namespace Aplicacion.Interfaces.Aplicacion
{
    public interface IServiciosSalas
    {
        Task<AsientosResponse> CapacidadDisponible(int idFuncion);
    }
}
