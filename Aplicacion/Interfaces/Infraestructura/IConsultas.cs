using Aplicacion.Casos_de_usos;
using Aplicacion.DTO;
using Dominio;

namespace Aplicación.Interfaces.Infraestructura
{
    public interface IConsultas
    {
        List<CarteleraDTO> ListarFunciones(int? id, List<CarteleraDTO> result);
        List<CarteleraDTO> ListarPeliculas(int? id, List<CarteleraDTO> result);
        List<CarteleraDTO> ListarGeneros(int? id, List<CarteleraDTO> result);
        List<CarteleraDTO> ListarFecha(DateTime? fecha, List<CarteleraDTO> result);
        List<CarteleraDTO> ListarFunciones();
    }
}
