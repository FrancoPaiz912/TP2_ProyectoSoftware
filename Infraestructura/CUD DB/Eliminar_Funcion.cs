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
        { //Se ekimina la función siempre y cuando no cuente con tickets vendidos.
           Funciones result = await _Contexto.Funciones.Include(s => s.Tickets)
                .Where(s => s.FuncionesId == funcion.FuncionesId && s.Tickets.Count() == 0).FirstOrDefaultAsync();
            if (result == null) //La función tiene tickets vendidos? No se puede eliminar
            {
                return false;
            }
            _Contexto.Remove(funcion); //Si no tiene tickets vendidos se elimina normalmente.
            await _Contexto.SaveChangesAsync();
            return true;
        }
    }
}
