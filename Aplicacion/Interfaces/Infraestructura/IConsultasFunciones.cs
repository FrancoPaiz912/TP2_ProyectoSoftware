using Dominio;

namespace Aplicación.Interfaces.Infraestructura
{
    public interface IConsultasFunciones
    {
        Task<List<Funciones>> ListarFunciones(string? fecha, string titulo, int? Genero);
        Task<Funciones> RetornarRegistro();
        Task<List<bool>> GetIDs(int IdPelicula, int IdSala);
        Task<Funciones> GetIdFuncion(int id);
        Task<bool> ComprobacionHoraria(int Salaid, DateTime Fecha, TimeSpan Hora);
    }
}
