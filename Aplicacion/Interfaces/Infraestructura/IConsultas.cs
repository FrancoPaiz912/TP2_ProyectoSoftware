using Aplicacion.Casos_de_usos;
using Aplicacion.DTO;
using Dominio;

namespace Aplicación.Interfaces.Infraestructura
{
    public interface IConsultas
    {
        Task<List<CarteleraDTO>> ListarFunciones(int? id, List<CarteleraDTO> result);
        Task<List<CarteleraDTO>> ListarPeliculas(int? id, List<CarteleraDTO> result);
        Task<List<CarteleraDTO>> ListarGeneros(int? id, List<CarteleraDTO> result);
        Task<List<CarteleraDTO>> ListarFecha(DateTime? fecha, List<CarteleraDTO> result);
        Task<List<CarteleraDTO>> ListarFunciones();
        Task<List<bool>> GetIDs(int IdPelicula, int IdSala);
        Task<Funciones> GetIdFuncion(int id);
        Task<bool> ComprobacionHoraria(int Salaid, DateTime Fecha, TimeSpan Hora);
    }
}
