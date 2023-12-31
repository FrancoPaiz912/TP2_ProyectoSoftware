﻿using Aplicación.Interfaces.Infraestructura;
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

        async Task<Funciones> IAgregar.AgregarTicket(Tickets ticket)
        {
            _Contexto.Add(ticket);
            await _Contexto.SaveChangesAsync();
            return await _Contexto.Funciones.Include(s => s.Tickets)
                .Include(s => s.Salas)
                .Include(s => s.Peliculas)
                .ThenInclude(s => s.Generos)
                .FirstOrDefaultAsync(s => s.FuncionId == ticket.FuncionId);
        }
    }
}
