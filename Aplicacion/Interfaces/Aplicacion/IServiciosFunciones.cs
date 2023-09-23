using Aplicacion.DTO;
using Aplicacion.RespuestasHTTP;
using Dominio;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Aplicacion
{
    public interface IServiciosFunciones
    {
        Task<List<CarteleraDTO>> GetFuncionesDia(DateTime? dia, List<CarteleraDTO> result);
        Task<List<CarteleraDTO>> GetFuncionesNombrePelicula(int? id, List<CarteleraDTO> result);
        Task<List<CarteleraDTO>> GetFuncionesGenero(int? id, List<CarteleraDTO> result);
        Task<List<CarteleraDTO>> GetFuncionesDTO();
        Task AddFunciones(Funciones funcion);
        Task<List<bool>> GetId(int IdPelicula, int IdSala);
        Task<Funciones> ComprobarFunciones(int id);
        Task<bool> EliminarFuncion(Funciones funcion);
        Task<bool> ComprobarHorario(int Salaid, DateTime Fecha, TimeSpan Hora);
        Task<TicketRespuesta> GenerarTicket(TicketDTO ticket);
        Task<FuncionRespuesta> GetDatosFuncion(int id);
    }
}
