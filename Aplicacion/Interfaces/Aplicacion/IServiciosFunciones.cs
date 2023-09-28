using Aplicacion.DTO;
using Aplicacion.RespuestasHTTP;
using Dominio;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Aplicacion
{
    public interface IServiciosFunciones
    {
        Task<List<FuncionRespuesta>> GetFuncionesRespuesta(string fecha,string titulo,int? Genero);
        Task<List<FuncionRespuesta>> GetCartelera(List<FuncionRespuesta> Funciones);
        Task<FuncionRespuesta> AddFunciones(FuncionesDTO funcion);
        Task<List<bool>> GetId(int IdPelicula, int IdSala);
        Task<Funciones> ComprobarFunciones(int id);
        Task<FuncionEliminadaResponse> EliminarFuncion(Funciones funcion);
        Task<bool> ComprobarHorario(int Salaid, DateTime Fecha, TimeSpan Hora);
        Task<TicketRespuesta> GenerarTicket(int ID, TicketDTO ticket);
        Task<FuncionRespuesta> GetDatosFuncion(int id);
    }
}
