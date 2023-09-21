namespace Aplicacion.DTO
{
    public class Cartelera
    {
        public string Titulo { get; set; }
        public string Sinopsis { get; set; }
        public string Poster { get; set; }
        public string Trailer { get; set; }
        public string Sala { get; set; }
        public int Capacidad { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string genero { get; set; }
    }
}
