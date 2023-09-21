using Aplicacion.DTO;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Aplicacion
{
    public interface IServiciosFunciones
    {
        //void ObtenerFunciones();
        //void IntroducirFuncion();
        Task<List<FuncionesDTO>> GetFuncionesDia(DateTime? dia, List<FuncionesDTO> result);
        Task<List<FuncionesDTO>> GetFuncionesNombrePelicula(int? id, List<FuncionesDTO> result);
        Task<List<FuncionesDTO>> GetFuncionesGenero(int? id, List<FuncionesDTO> result);
        Task<List<FuncionesDTO>> GetFunciones();
    }
}
