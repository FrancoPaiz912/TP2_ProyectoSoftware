
namespace Aplicacion.Interfaces.Infraestructura
{
    public interface IConsultasSalas
    {
        Task<int> CapacidadDisponible(int idFuncion);
    }
}
