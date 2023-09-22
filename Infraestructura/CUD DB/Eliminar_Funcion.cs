using Aplicacion.Interfaces.Infraestructura;
using Dominio;
using Infraestructura.EstructuraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.CUD_DB
{
    public class Eliminar_Funcion : IEliminar
    {
        private readonly Contexto_Cine _Contexto;

        public Eliminar_Funcion(Contexto_Cine contexto)
        {
            _Contexto = contexto;
        }

        async Task IEliminar.RemoverFuncion(Funciones funcion)
        {
            _Contexto.Remove(funcion);
            await _Contexto.SaveChangesAsync();
        }
    }
}
