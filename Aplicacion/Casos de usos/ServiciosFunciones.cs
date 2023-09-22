using Aplicacion.DTO;
using Aplicacion.Excepciones;
using Aplicacion.Interfaces.Aplicacion;
using Aplicación.Interfaces.Infraestructura;
using Dominio;
using System.Security.Cryptography;

namespace Aplicacion.Casos_de_usos
{
    public class ServiciosFunciones : IServiciosFunciones
    {
        private readonly IConsultas _consultas;
        private readonly IAgregar _Agregar;
        public ServiciosFunciones(IConsultas consultas, IAgregar Agregar)
        {
            _consultas = consultas;
            _Agregar = Agregar;
        }

        //Gets

        async Task<List<CarteleraDTO>> IServiciosFunciones.GetFunciones()
        {
            List<CarteleraDTO> acompanante = _consultas.ListarFunciones();
            return acompanante;
        }

        async Task<List<CarteleraDTO>> IServiciosFunciones.GetFuncionesDia(DateTime? dia, List<CarteleraDTO> result)
        {
            if (result.Count() == 0 && dia != null)
            {
                result = _consultas.ListarFecha(dia, result);
                return result;
            }
            else
            {
                result = result.Where(s => s.Fecha == dia).ToList();  
                return result;
            }
        }

        async Task<List<CarteleraDTO>> IServiciosFunciones.GetFuncionesNombrePelicula(int? PeliculaID, List<CarteleraDTO> result)
        {
            if (result.Count() == 0 && PeliculaID != null)
            {
                result = _consultas.ListarPeliculas(PeliculaID, result);
                return result;
            }
            else
            {
                result = result.Where(s => s.PeliculaId == PeliculaID).ToList();  
                return result;
            }
        }

        async Task<List<CarteleraDTO>> IServiciosFunciones.GetFuncionesGenero(int? GeneroID, List<CarteleraDTO> result)
        {
            if (result.Count() == 0 && GeneroID != null)
            {
                result = _consultas.ListarGeneros(GeneroID, result);
                return result;
            }
            else
            {
                result = result.Where(s => s.GenerosId == GeneroID).ToList();
                return result;
            }
        }

        async Task IServiciosFunciones.AddFunciones(Funciones funcion)
        {
            _Agregar.AgregarFuncion(funcion);

        }
    }
}
