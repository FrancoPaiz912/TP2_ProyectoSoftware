using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTO
{
    public class FuncionesDTO
    {
        public int FuncionesId { get; set; }
        public int PeliculaId { get; set; }
        
        //public int SalaId { get; set; } 
        public int GenerosId { get; set; }
        public DateTime Fecha { get; set; }
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public string Poster { get; set; }
        public string Trailer { get; set; }
        public string Sala { get; set; }
        public int Capacidad { get; set; }
        public TimeSpan Hora { get; set; }
        public string genero { get; set; }
    }
}
