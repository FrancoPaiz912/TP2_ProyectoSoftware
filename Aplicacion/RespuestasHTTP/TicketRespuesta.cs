using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.RespuestasHTTP
{
    public class TicketRespuesta
    {
        public string Usuario { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Sala { get; set; }
        public string genero { get; set; }
    }
}
