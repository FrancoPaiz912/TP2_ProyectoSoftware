using Aplicacion.DTO;
using Aplicacion.RespuestasHTTP;
using Dominio;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Aplicacion
{
    public interface IServiciosFunciones
    {
        Task<List<FuncionCompletaRespuesta>> GetFuncionesDia(DateTime dia, List<FuncionCompletaRespuesta> result);
        Task<List<FuncionCompletaRespuesta>> GetFuncionesNombrePelicula(string? Pelicula, List<FuncionCompletaRespuesta> result);
        Task<List<FuncionCompletaRespuesta>> GetFuncionesGenero(int? id, List<FuncionCompletaRespuesta> result);
        Task<List<FuncionCompletaRespuesta>> GetFuncionesRespuesta();
        Task<List<FuncionCompletaRespuesta>> GetCartelera(List<FuncionCompletaRespuesta> Funciones);
        Task<FuncionCompletaRespuesta> AddFunciones(FuncionesDTO funcion);
        Task<List<bool>> GetId(int IdPelicula, int IdSala);
        Task<Funciones> ComprobarFunciones(int id);
        Task<EliminarFuncionResponse> EliminarFuncion(Funciones funcion);
        Task<bool> ComprobarHorario(int Salaid, DateTime Fecha, TimeSpan Hora);
        Task<TicketRespuesta> GenerarTicket(int ID, TicketDTO ticket);
        Task<FuncionCompletaRespuesta> GetDatosFuncion(int id);
    }
}
