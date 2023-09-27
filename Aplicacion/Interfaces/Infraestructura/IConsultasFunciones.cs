using Aplicacion.Casos_de_usos;
using Aplicacion.DTO;
using Aplicacion.RespuestasHTTP;
using Dominio;

namespace Aplicación.Interfaces.Infraestructura
{
    public interface IConsultasFunciones
    {
        Task<List<Funciones>> ListarFunciones(int? id, List<FuncionCompletaRespuesta> result);
        Task<List<Funciones>> ListarPeliculas(string? id, List<FuncionCompletaRespuesta> result);
        Task<List<Funciones>> ListarGeneros(int? id, List<FuncionCompletaRespuesta> result);
        Task<List<Funciones>> ListarFecha(DateTime fecha, List<FuncionCompletaRespuesta> result);
        Task<List<Funciones>> ListarFunciones();
        Task<Funciones> RetornarRegistro();
        Task<List<bool>> GetIDs(int IdPelicula, int IdSala);
        Task<Funciones> GetIdFuncion(int id);
        Task<bool> ComprobacionHoraria(int Salaid, DateTime Fecha, TimeSpan Hora);
    }
}
