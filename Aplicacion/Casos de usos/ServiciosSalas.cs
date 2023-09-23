using Aplicacion.Interfaces.Aplicacion;
using Aplicacion.Interfaces.Infraestructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Casos_de_usos
{
    public class ServiciosSalas : IServiciosSalas
    {
        private readonly IConsultasSalas _Consultas;
        public ServiciosSalas(IConsultasSalas Servicios) 
        {
            _Consultas = Servicios;
        }

        async Task<int> IServiciosSalas.CapacidadDisponible(int IdFuncion)
        {
            return await _Consultas.CapacidadDisponible(IdFuncion);
        }
    }
}
