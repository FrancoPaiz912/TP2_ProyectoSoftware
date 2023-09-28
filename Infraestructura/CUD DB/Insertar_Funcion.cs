using Aplicación.Interfaces.Infraestructura;
using Dominio;
using Infraestructura.EstructuraDB;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Inserts
{
    public class Insertar_Funcion : IAgregar
    {
        private readonly Contexto_Cine _Contexto;

        public Insertar_Funcion(Contexto_Cine contexto)
        {
            _Contexto = contexto;
        }

        async Task IAgregar.AgregarFuncion(Funciones funcion)
        {
            _Contexto.Add(funcion);
            await _Contexto.SaveChangesAsync();
        }

        async Task<Tickets> IAgregar.AgregarTicket(Tickets ticket)
        {//Agregamops los ticckets a la base de datos y devolvemos los datos de la función relacionada 
            _Contexto.Add(ticket);
            await _Contexto.SaveChangesAsync();
            return await _Contexto.Tickets.Include(s => s.Funciones)
                .ThenInclude(s => s.Salas)
                .ThenInclude(s => s.Funciones)
                .ThenInclude(s => s.Peliculas)
                .ThenInclude(s => s.Generos)
                .FirstOrDefaultAsync(s=> s.Funciones.FuncionesId == ticket.FuncionId);
        }
    }
}
