using Aplicacion.DTO;
using Dominio;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Aplicacion
{
    public interface IServiciosFunciones
    {
        //void ObtenerFunciones();
        //void IntroducirFuncion();
        Task<List<CarteleraDTO>> GetFuncionesDia(DateTime? dia, List<CarteleraDTO> result);
        Task<List<CarteleraDTO>> GetFuncionesNombrePelicula(int? id, List<CarteleraDTO> result);
        Task<List<CarteleraDTO>> GetFuncionesGenero(int? id, List<CarteleraDTO> result);
        Task<List<CarteleraDTO>> GetFunciones();
        Task AddFunciones(Funciones funcion);
    }
}
