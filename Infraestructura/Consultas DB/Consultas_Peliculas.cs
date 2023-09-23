using Aplicacion.Interfaces.Infraestructura;
using Dominio;
using Infraestructura.EstructuraDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infraestructura.Consultas_DB
{
    public class Consultas_Peliculas : IConsultasPeliculas
    {
        private readonly Contexto_Cine _Contexto;

        public Consultas_Peliculas(Contexto_Cine contexto)
        {
            _Contexto = contexto;
        }

        async Task<bool> IConsultasPeliculas.ComprobarNombre(string nombre)
        {
            if (await _Contexto.Peliculas.FirstOrDefaultAsync(s => s.Titulo == nombre) == null)
            {
                return false;
            }
            else return true;
        }

        async Task<bool> IConsultasPeliculas.ComprobarID(int id)
        {
            if (await _Contexto.Peliculas.FirstOrDefaultAsync(s => s.Peliculasid == id) == null)
            {
                return true;
            }
            else return false;
        }
    }
}
