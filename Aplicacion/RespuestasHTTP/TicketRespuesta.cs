using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.RespuestasHTTP
{
    public class TicketRespuesta
    {
        public List<Guid> tickets { get; set; }
        public FuncionCompletaRespuesta Funciones { get; set; }
        public string usuario { get; set; }
    }
}
