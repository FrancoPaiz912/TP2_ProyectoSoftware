using Aplicacion.DTO;
using Dominio;

namespace Aplicación.Interfaces.Infraestructura
{
    public interface IAgregar
    {
        Task AgregarFuncion(Funciones funcion);

        Task<Tickets> AgregarTicket(Tickets ticket);
    }
}
