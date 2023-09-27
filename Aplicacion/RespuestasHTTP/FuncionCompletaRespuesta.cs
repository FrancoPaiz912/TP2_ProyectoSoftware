using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.RespuestasHTTP
{
    public class FuncionCompletaRespuesta
    {
        public int FuncionId { get; set; }
        public PeliculaRespuesta Pelicula { get; set; }
        public SalaRespuesta Sala { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Horario { get; set; }
    }
}
