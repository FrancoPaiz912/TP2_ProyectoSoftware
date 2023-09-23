using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces.Infraestructura
{
    public interface IEliminar
    {
        Task<bool> RemoverFuncion(Funciones funcion);
    }
}
