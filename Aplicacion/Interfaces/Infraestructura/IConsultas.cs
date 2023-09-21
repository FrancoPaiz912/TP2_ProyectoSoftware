using Aplicacion.Casos_de_usos;
using Aplicacion.DTO;
using Dominio;

namespace Aplicación.Interfaces.Infraestructura
{
    public interface IConsultas
    {
        List<FuncionesDTO> ListarFunciones(int? id, List<FuncionesDTO> result);
        List<FuncionesDTO> ListarPeliculas(int? id, List<FuncionesDTO> result);
        List<FuncionesDTO> ListarGeneros(int? id, List<FuncionesDTO> result);
        List<FuncionesDTO> ListarFecha(DateTime? fecha, List<FuncionesDTO> result);
        List<FuncionesDTO> ListarFunciones();
    }
}
