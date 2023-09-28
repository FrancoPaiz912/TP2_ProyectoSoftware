using Aplicacion.RespuestasHTTP;

namespace Aplicacion.Interfaces.Aplicacion
{
    public interface IServiciosSalas
    {
        Task<AsientosRespuesta> CapacidadDisponible(int idFuncion);
    }
}
