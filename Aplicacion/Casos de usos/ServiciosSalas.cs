using Aplicacion.Interfaces.Aplicacion;
using Aplicacion.Interfaces.Infraestructura;
using Aplicacion.RespuestasHTTP;

namespace Aplicacion.Casos_de_usos
{
    public class ServiciosSalas : IServiciosSalas
    {
        private readonly IConsultasSalas _Consultas;
        public ServiciosSalas(IConsultasSalas Servicios) 
        {
            _Consultas = Servicios;
        }

        async Task<AsientosRespuesta> IServiciosSalas.CapacidadDisponible(int IdFuncion)
        {
            return new AsientosRespuesta 
            {
                Cantidad = await _Consultas.CapacidadDisponible(IdFuncion), //Comprueba y devuelve la cantidad de entradas disponibles
            };
        }
    }
}
