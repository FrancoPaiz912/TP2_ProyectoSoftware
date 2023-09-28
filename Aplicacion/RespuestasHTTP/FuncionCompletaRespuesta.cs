
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
