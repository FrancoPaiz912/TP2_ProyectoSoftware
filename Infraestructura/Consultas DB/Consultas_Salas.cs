using Aplicacion.Interfaces.Infraestructura;
using Dominio;
using Infraestructura.EstructuraDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Consultas_DB
{
    public class Consultas_Salas : IConsultasSalas
    {
        private readonly Contexto_Cine _Contexto;

        public Consultas_Salas(Contexto_Cine contexto) 
        {
            _Contexto = contexto;
        }

        async Task<int> IConsultasSalas.CapacidadDisponible(int idFuncion) //Tener todas las querys juntas?
        {
            int result=  _Contexto.Funciones             
                        .Include(s => s.Tickets)
                        .Include(s => s.Salas).Where(s => s.FuncionesId == idFuncion).Select(s => s.Salas.Capacidad - s.Tickets.Count()).FirstOrDefault();
            return result;
        }
    }
}
