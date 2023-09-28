using Dominio;

namespace Aplicacion.Interfaces.Infraestructura
{
    public interface IEliminar
    {
        Task<bool> RemoverFuncion(Funciones funcion);
    }
}
