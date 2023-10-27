using Dominio;

namespace Aplicación.Interfaces.Infraestructura
{
    public interface IAgregar
    {
        Task AgregarFuncion(Funciones funcion);

        Task<Funciones> AgregarTicket(Tickets ticket);
    }
}
