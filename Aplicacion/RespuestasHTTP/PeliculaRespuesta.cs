using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.RespuestasHTTP
{
    public class PeliculaRespuesta
    {
        public int Peliculaid { get; set; }
        public string Titulo { get; set; }
        public string Poster { get; set; }
        public GeneroRespuesta Genero { get; set; }
    }
}
