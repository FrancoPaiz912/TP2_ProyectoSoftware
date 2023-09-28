using Aplicacion.Interfaces.Infraestructura;
using Dominio;
using Infraestructura.EstructuraDB;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Consultas_DB
{
    public class Consultas_Salas : IConsultasSalas
    {
        private readonly Contexto_Cine _Contexto;

        public Consultas_Salas(Contexto_Cine contexto) 
        {
            _Contexto = contexto;
        }

        async Task<int> IConsultasSalas.CapacidadDisponible(int idFuncion) 
        {//Consulta de la capacidad disponible para determinada funcion
            int result=  _Contexto.Funciones             
                        .Include(s => s.Tickets)
                        .Include(s => s.Salas).Where(s => s.FuncionesId == idFuncion).Select(s => s.Salas.Capacidad - s.Tickets.Count()).FirstOrDefault(); //No me descuenta los tickets
            return result;
        }
    }
}
