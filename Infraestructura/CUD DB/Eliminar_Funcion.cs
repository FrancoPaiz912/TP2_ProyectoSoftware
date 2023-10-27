using Aplicacion.Interfaces.Infraestructura;
using Dominio;
using Infraestructura.EstructuraDB;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.CUD_DB
{
    public class Eliminar_Funcion : IEliminar
    {
        private readonly Contexto_Cine _Contexto;

        public Eliminar_Funcion(Contexto_Cine contexto)
        {
            _Contexto = contexto;
        }

        async Task<bool> IEliminar.RemoverFuncion(Funciones funcion)
        { 
            Funciones result = await _Contexto.Funciones.Include(s => s.Tickets)
                 .Where(s => s.FuncionId == funcion.FuncionId && s.Tickets.Count() == 0).FirstOrDefaultAsync();
            if (result == null) 
            {
                return false;
            }
            _Contexto.Remove(funcion); 
            await _Contexto.SaveChangesAsync();
            return true;
        }
    }
}
