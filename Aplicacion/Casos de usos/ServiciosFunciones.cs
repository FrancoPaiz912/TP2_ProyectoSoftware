using Aplicacion.DTO;
using Aplicacion.Excepciones;
using Aplicacion.Interfaces.Aplicacion;
using Aplicación.Interfaces.Infraestructura;
using Aplicacion.Interfaces.Infraestructura;
using Dominio;
using System.Security.Cryptography;

namespace Aplicacion.Casos_de_usos
{
    public class ServiciosFunciones : IServiciosFunciones
    {
        private readonly IConsultas _consultas;
        private readonly IAgregar _Agregar;
        private readonly IEliminar _Eliminar;
        public ServiciosFunciones(IConsultas consultas, IAgregar Agregar, IEliminar Eliminar)
        {
            _consultas = consultas;
            _Agregar = Agregar;
            _Eliminar = Eliminar;
        }

        //Gets

        async Task<List<CarteleraDTO>> IServiciosFunciones.GetFunciones()
        {
            List<CarteleraDTO> acompanante = await _consultas.ListarFunciones();
            return acompanante;
        }

        async Task<List<CarteleraDTO>> IServiciosFunciones.GetFuncionesDia(DateTime? dia, List<CarteleraDTO> result)
        {
            if (result.Count() == 0 && dia != null)
            {
                result = await _consultas.ListarFecha(dia, result);
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
                result = await _consultas.ListarPeliculas(PeliculaID, result);
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
                result = await _consultas.ListarGeneros(GeneroID, result);
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

        async Task<List<bool>> IServiciosFunciones.GetId(int IdPelicula,int IdSala)
        {
            return await _consultas.GetIDs(IdPelicula,IdSala);
        }

        async Task<Funciones> IServiciosFunciones.ComprobarFunciones(int id)
        {
            return await _consultas.GetIdFuncion(id);
        }

        async Task IServiciosFunciones.EliminarFuncion(Funciones funcion)
        {
            await _Eliminar.RemoverFuncion(funcion);
        }

        async Task<bool> IServiciosFunciones.ComprobarHorario(int Salaid, DateTime Fecha, TimeSpan Hora)
        {
            return await _consultas.ComprobacionHoraria(Salaid, Fecha, Hora);
        }
    }
}
